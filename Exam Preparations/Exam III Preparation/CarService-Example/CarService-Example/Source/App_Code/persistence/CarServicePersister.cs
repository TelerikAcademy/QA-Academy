using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;
using System.Data.Objects;

namespace persistence
{
    /// <summary>
    /// Implements ICarServicePersister persistence facade.
    /// </summary>
    public class CarServicePersister : ICarServicePersister
    {
        private Entities carServiceEntities;

        /// <summary>
        /// Creates object context and opens DB connection.
        /// </summary>
        public CarServicePersister()
        {
            this.carServiceEntities = new Entities();
        }

        public void CreateAutomobile(Automobile automobile)
        {
            this.carServiceEntities.Automobiles.AddObject(automobile);
        }

        public Automobile GetAutomobilById(int automobileId)
        {
            Automobile foundAutomobile = this.carServiceEntities.Automobiles.FirstOrDefault(automobile => automobile.AutomobileId == automobileId);
            return foundAutomobile;
        }

        public IQueryable<Automobile> GetAutomobiles()
        {
            return this.carServiceEntities.Automobiles;
        }

        public IQueryable<Automobile> GetAutomobilesByVinChassis(string vinChassis)
        {
            IQueryable<Automobile> automobiles =
                from automobile in this.carServiceEntities.Automobiles
                where ((automobile.ChassisNumber.IndexOf(vinChassis) >= 0) ||
                        (automobile.Vin.IndexOf(vinChassis) >= 0))
                select automobile;
            return automobiles;
        }

        public void DeleteAutomobile(Automobile automobile)
        {
            this.carServiceEntities.Automobiles.DeleteObject(automobile);
        }

        public bool IsChassisNumberExists(string chasshisNumber)
        {
            bool chassisExists = this.carServiceEntities.Automobiles.Any(currAuto => currAuto.ChassisNumber.Equals(chasshisNumber));
            return chassisExists;
        }

        public bool IsVinExists(string vin)
        {            
            bool vinExists = this.carServiceEntities.Automobiles.Any(currAuto => currAuto.Vin.Equals(vin));            
            return vinExists;
        }

        public void CreateSparePart(SparePart sparePart)
        {
            this.carServiceEntities.SpareParts.AddObject(sparePart);
        }

        public SparePart GetSparePartById(int sparePartId)
        {
            SparePart sparePart = this.carServiceEntities.SpareParts.FirstOrDefault(part => part.PartId == sparePartId);
            return sparePart;
        }

        public IQueryable<SparePart> GetSpareParts()
        {
            return this.carServiceEntities.SpareParts;
        }

        public IQueryable<SparePart> GetActiveSpareParts()
        {
            IQueryable<SparePart> activeSpareParts = this.carServiceEntities.SpareParts.Where(part => part.IsActive == true);
            return activeSpareParts;
        }

        public int GetSparePartMaxId()
        {
            int partId = this.carServiceEntities.SpareParts.Max(sp => sp.PartId);
            return partId;
        }

        public void DeleteSparePart(SparePart sparePart)
        {
            this.carServiceEntities.DeleteObject(sparePart);
        }

        public void CreateRepairCard(RepairCard repairCard)
        {
            this.carServiceEntities.RepairCards.AddObject(repairCard);
        }

        public void DeleteRepairCard(RepairCard repairCard)
        {
            this.carServiceEntities.RepairCards.DeleteObject(repairCard);
        }

        public int GetRepairCardMaxId()
        {
            int repairCardId = this.carServiceEntities.RepairCards.Max(repairCard => repairCard.CardId);
            return repairCardId;
        }

        public RepairCard GetRepairCardById(int cardId)
        {
            RepairCard repairCard = this.carServiceEntities.RepairCards.FirstOrDefault(card => card.CardId == cardId);
            return repairCard;
        }

        public IQueryable<RepairCard> GetRepairCards()
        {
            return this.carServiceEntities.RepairCards;
        }

        public IQueryable<RepairCard> GetRepairCards(string vinChassis)
        {
            return GetRepairCardsByVinChassis(vinChassis);
        }

        public IQueryable<RepairCard> GetUnfinishedRepairCards(DateTime? startRepair, string vinChassis)
        {
            IQueryable<RepairCard> unfinishedRepairCards = null;
            if (startRepair.HasValue == true && string.IsNullOrEmpty(vinChassis) == false)
            {
                unfinishedRepairCards = GetUnfinishedRepairCardsByVinChassisStartDate(startRepair.Value, vinChassis);
            }
            else if (startRepair.HasValue == true && string.IsNullOrEmpty(vinChassis) == true)
            {
                unfinishedRepairCards = GetUnfinishedRepairCards(startRepair.Value);
            }
            else if (startRepair.HasValue == false && string.IsNullOrEmpty(vinChassis) == false)
            {
                unfinishedRepairCards = GetUnfinishedRepairCardsByVinChassis(vinChassis);
            }
            else if (startRepair.HasValue == false && string.IsNullOrEmpty(vinChassis) == true)
            {
                unfinishedRepairCards = GetUnfinishedRepairCards();
            }
            return unfinishedRepairCards;
        }

        public IQueryable<RepairCard> GetFinishedRepairCards(DateTime? fromFinishRepair, DateTime? toFinishRepair)
        {
            IQueryable<RepairCard> unfinishedRepairCards = null;
            if (fromFinishRepair.HasValue == false && toFinishRepair.HasValue == false)
            {
                unfinishedRepairCards =
                from repairCard in this.carServiceEntities.RepairCards
                where (repairCard.FinishRepair != null)
                select repairCard;
            }
            else if (fromFinishRepair.HasValue == false && toFinishRepair.HasValue == true)
            {
                unfinishedRepairCards =
                from repairCard in this.carServiceEntities.RepairCards
                where (repairCard.FinishRepair != null && 
                    repairCard.FinishRepair <= toFinishRepair)
                select repairCard;
            }
            else if (fromFinishRepair.HasValue == true && toFinishRepair.HasValue == false)
            {
                unfinishedRepairCards =
                from repairCard in this.carServiceEntities.RepairCards
                where (repairCard.FinishRepair != null && 
                    repairCard.FinishRepair >= fromFinishRepair)
                select repairCard;
            }
            else if (fromFinishRepair.HasValue == true && toFinishRepair.HasValue == true)
            {
                unfinishedRepairCards =
                from repairCard in this.carServiceEntities.RepairCards
                where (repairCard.FinishRepair != null &&
                    repairCard.FinishRepair >= fromFinishRepair &&
                    repairCard.FinishRepair <= toFinishRepair)
                select repairCard;
            }            
            return unfinishedRepairCards;
        }

        public void SaveChanges()
        {
            this.carServiceEntities.SaveChanges();
        }

        public void ReleaseConnection()
        {
            if (this.carServiceEntities != null){this.carServiceEntities.Dispose();}
        }

        private IQueryable<RepairCard> GetRepairCardsByVinChassis(string vinChassis)
        {
            IQueryable<RepairCard> repairCards =
                from repairCard in this.carServiceEntities.RepairCards
                where ((repairCard.Automobile.ChassisNumber.IndexOf(vinChassis) >= 0) ||
                    (repairCard.Automobile.Vin.IndexOf(vinChassis) >= 0))
                select repairCard;
            return repairCards;
        }

        private IQueryable<RepairCard> GetUnfinishedRepairCards()
        {
            IQueryable<RepairCard> unfinishedRepairCards =
                from repairCard in this.carServiceEntities.RepairCards
                where (repairCard.FinishRepair == null)
                select repairCard;
            return unfinishedRepairCards;
        }

        private IQueryable<RepairCard> GetUnfinishedRepairCards(DateTime startRepair)
        {
            IQueryable<RepairCard> unfinishedRepairCards =
                from repairCard in this.carServiceEntities.RepairCards
                where (repairCard.StartRepair == startRepair &&
                    repairCard.FinishRepair == null)
                select repairCard;
            return unfinishedRepairCards;
        }

        private IQueryable<RepairCard> GetUnfinishedRepairCardsByVinChassis(string vinChassis)
        {
            IQueryable<RepairCard> unfinishedRepairCards =
                from repairCard in this.carServiceEntities.RepairCards
                where (repairCard.FinishRepair == null &&
                    ((repairCard.Automobile.ChassisNumber.IndexOf(vinChassis) >= 0) ||
                        (repairCard.Automobile.Vin.IndexOf(vinChassis) >= 0)))
                select repairCard;
            return unfinishedRepairCards;
        }

        private IQueryable<RepairCard> GetUnfinishedRepairCardsByVinChassisStartDate(DateTime startRepair, string vinChassis)
        {
            IQueryable<RepairCard>  unfinishedRepairCards =
                from repairCard in this.carServiceEntities.RepairCards
                where (repairCard.StartRepair == startRepair &&
                    repairCard.FinishRepair == null &&
                    ((repairCard.Automobile.ChassisNumber.IndexOf(vinChassis) >= 0) ||
                    (repairCard.Automobile.Vin.IndexOf(vinChassis) >= 0)))
                select repairCard;
            return unfinishedRepairCards;
        }
    }
}


