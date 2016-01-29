using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using persistence;
using System.Collections;
using constants;
using System.Globalization;
using System.Web.Security;
using businesslogic.utils;
using System.Text;
using presentation.utils;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace presentation
{
    public partial class MembersAddRepairCard : System.Web.UI.Page
    {
        ICarServicePersister persister;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.persister == null)
            {
                this.persister = new CarServicePersister();
            }
            if (IsPostBack == false)
            {
                this.finishRepairDate.Enabled = false;
                object repairCardIdObject = Session[CarServiceConstants.REPAIR_CARD_ID_PARAM_NAME];
                if (repairCardIdObject != null)
                {
                    int repairCardId;
                    if (Int32.TryParse(repairCardIdObject.ToString(), out repairCardId))
                    {
                        RepairCard repairCard = this.persister.GetRepairCardById(repairCardId);
                        LoadRepairCardInfo(repairCard);

                        Guid creatorUserId = repairCard.UserId;
                        MembershipUser currentUser = Membership.GetUser();
                        Guid currentUserId = (Guid)currentUser.ProviderUserKey;
                        if (currentUserId.Equals(creatorUserId) == false)
                        {
                            DisableAllInputControls();
                        }
                        else
                        {                            
                            this.startRepairDate.Enabled = false;
                        }                        
                    }
                }
                else
                {
                    IQueryable<SparePart> spareParts = this.persister.GetActiveSpareParts();
                    object customSpareParts = CarServicePresentationUtility.GetSparePartsFormatForListBox(spareParts);
                    CarServicePresentationUtility.BindListBox(this.unselectedSpareParts, customSpareParts);
                    this.startRepairDate.SelectedDate =
                        DateTime.Now.ToString(CarServiceConstants.DATE_FORMAT, new CultureInfo(CarServiceConstants.ENGLISH_CULTURE_INFO));
                    this.finishRepairDate.Enabled = false;
                    this.operatorLbl.Text = this.User.Identity.Name;
                }            
            }
            CarServicePresentationUtility.ClearNotificationMsgList(this.notificationMsgList);
            CarServicePresentationUtility.HideNotificationMsgList(this.notificationMsgList);
            Session[CarServiceConstants.AUTOMOBILE_ID_REQUEST_PARAM_NAME] = null;
        }

        protected void SearchAutomobile_OnClick(object sender, EventArgs e)
        {
            string vinChassis = this.VinChassisTxt.Text;
            if (string.IsNullOrEmpty(vinChassis) == false)
            {
                IQueryable<Automobile> foundAutomobiles = this.persister.GetAutomobilesByVinChassis(vinChassis);
                var customAutomobileFormat = 
                    from auto in foundAutomobiles
                    select new
                    {
                        AutomobileId = auto.AutomobileId,
                        AutomobileRepresentation = auto.Vin + " / " + auto.ChassisNumber
                    };
                this.automobileDropDown.DataSource = customAutomobileFormat;
                this.automobileDropDown.DataBind();
            }
        }

        protected void SelectSpareParts_OnClick(object sender, EventArgs e)
        {
            decimal partsPrice = 0M;
            CarServicePresentationUtility.MoveListItems(this.unselectedSpareParts, this.selectedSpareParts, false, this.persister, out partsPrice);
            this.sparePartsPrice.Text = partsPrice.ToString();
            this.repairPrice.Text = partsPrice.ToString();
        }

        protected void UnselectSpareParts_OnClick(object sender, EventArgs e)
        {
            decimal partsPrice = 0M;
            CarServicePresentationUtility.MoveListItems(this.selectedSpareParts, this.unselectedSpareParts, true, this.persister, out partsPrice);
            this.sparePartsPrice.Text = partsPrice.ToString();
            this.repairPrice.Text = partsPrice.ToString();
        }

        protected void CancelRepairCard_OnClick(object sender, EventArgs e)
        {
            CarServiceUtility.ClearSessionAttributes(Session);
            string continueUrl = "~/Members/RepairCards/RepairCards.aspx";
            Response.Redirect(continueUrl);
        }

        protected void SaveRepairCard_OnClick(object sender, EventArgs e)
        {
            CarServicePresentationUtility.ClearNotificationMsgList(this.notificationMsgList);
            CarServicePresentationUtility.HideNotificationMsgList(this.notificationMsgList);
            this.notificationMsgList.CssClass = CarServiceConstants.NEGATIVE_CSS_CLASS_NAME;

            DateTime? startRepairDate = null;
            string startRepairDateTxt = this.startRepairDate.SelectedDate;
            bool validStartRepairDate = CarServicePresentationUtility.ProcessStartRepairDate(startRepairDateTxt, 
                this.notificationMsgList, out startRepairDate);

            decimal sparePartsPrice = 0M;
            decimal repairPrice = 0M;
            string repairPriceTxt = this.repairPrice.Text;
            string sparePartsPriceTxt = this.sparePartsPrice.Text;
            bool validPrices = CarServicePresentationUtility.ProcessRepairPrices(sparePartsPriceTxt, repairPriceTxt, 
                this.notificationMsgList, out sparePartsPrice, out repairPrice);
            
            string automobileIdTxt = this.automobileDropDown.SelectedValue;
            Automobile automobile = CarServiceUtility.GetAutomobile(automobileIdTxt, this.persister);
            bool validAutomobileId = (automobile != null);

            ListItemCollection selectedSparePartItems = this.selectedSpareParts.Items;
            bool validSpareParts = CarServicePresentationUtility.IsSparePartItemsValid(selectedSparePartItems, this.notificationMsgList);

            if (validAutomobileId && validPrices && validSpareParts &&
                (validStartRepairDate && startRepairDate.HasValue))
            {
                string description = this.repairCardDescription.Text;
                object repairCardIdObject = Session[CarServiceConstants.REPAIR_CARD_ID_PARAM_NAME];
                if (repairCardIdObject != null)
                {
                    int repairCardId;
                    if (Int32.TryParse(repairCardIdObject.ToString(), out repairCardId))
                    {
                        DateTime? finishRepairDate = null;
                        string finishRepairDateTxt = this.finishRepairDate.SelectedDate;
                        bool validFinishRepairDate = CarServicePresentationUtility.ProcessFinishRepairDate(finishRepairDateTxt,
                            this.notificationMsgList, out finishRepairDate);
                        if (validFinishRepairDate == true)
                        {
                            RepairCard repairCard = this.persister.GetRepairCardById(repairCardId);
                            UpdateRepairCard(repairCard, automobile, finishRepairDate, description,
                                sparePartsPrice, repairPrice, selectedSparePartItems);
                            CarServicePresentationUtility.AppendNotificationMsg("Repair card is updated successfully", this.notificationMsgList);
                            this.notificationMsgList.CssClass = CarServiceConstants.POSITIVE_CSS_CLASS_NAME;
                        }
                    }
                }
                else
                {
                    SaveRepairCard(automobile, startRepairDate.Value, description,
                        sparePartsPrice, repairPrice, selectedSparePartItems);
                    CarServicePresentationUtility.AppendNotificationMsg("Repair card is saved successfully", this.notificationMsgList);
                    this.notificationMsgList.CssClass = CarServiceConstants.POSITIVE_CSS_CLASS_NAME;
                }
            }
            CarServicePresentationUtility.ShowNotificationMsgList(this.notificationMsgList);             
        }

        protected void Automobile_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            int automobileId;
            string automobileIdTxt = e.Value;
            bool validAutomobile = Int32.TryParse(automobileIdTxt, out automobileId) && (automobileId >= 0);
            e.IsValid = validAutomobile;
        }

        protected void Price_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            double price;
            e.IsValid = Double.TryParse(e.Value, out price);
        }

        #region Private methods

        private void LoadRepairCardInfo(RepairCard repairCard)
        {
            this.repairCardIdLbl.Text = repairCard.CardId.ToString();
            this.operatorLbl.Text = repairCard.aspnet_Users.UserName;
            List<Automobile> automobiles = new List<Automobile>();
            automobiles.Add(repairCard.Automobile);
            var customAutomobileFormat =
                from auto in automobiles
                select new
                {
                    AutomobileId = auto.AutomobileId,
                    AutomobileRepresentation = auto.Vin + " / " + auto.ChassisNumber
                };
            this.automobileDropDown.DataSource = customAutomobileFormat;
            this.automobileDropDown.DataBind();

            this.sparePartsPrice.Text = repairCard.PartPrice.ToString();
            this.repairPrice.Text = repairCard.CardPrice.ToString();

            CultureInfo englishCultureInfo = new CultureInfo(CarServiceConstants.ENGLISH_CULTURE_INFO);
            this.startRepairDate.SelectedDate = repairCard.StartRepair.ToString(CarServiceConstants.DATE_FORMAT, englishCultureInfo);
            DateTime? finishRepairDate = repairCard.FinishRepair;
            if (finishRepairDate.HasValue)
            {
                this.finishRepairDate.SelectedDate = finishRepairDate.Value.ToString(CarServiceConstants.DATE_FORMAT, englishCultureInfo);
                DisableAllInputControls();
            }
            else
            {
                this.finishRepairDate.Enabled = true;
            }
            EntityCollection<SparePart> selectedParts = repairCard.SpareParts;
            List<SparePart> unselectedParts = new List<SparePart>();
            IQueryable<SparePart> activeSpareParts = this.persister.GetActiveSpareParts();
            foreach (SparePart part in activeSpareParts)
            {
                if (selectedParts.Contains(part) == false)                
                {
                    unselectedParts.Add(part);
                }
            }
            object customUnselectedSpareParts = CarServicePresentationUtility.GetSparePartsFormatForListBox(unselectedParts);
            object customSelectedSpareParts = CarServicePresentationUtility.GetSparePartsFormatForListBox(selectedParts);
            CarServicePresentationUtility.BindListBox(this.unselectedSpareParts, customUnselectedSpareParts);
            CarServicePresentationUtility.BindListBox(this.selectedSpareParts, customSelectedSpareParts);
            this.repairCardDescription.Text = repairCard.Description;
        }

        private void DisableAllInputControls()
        {
            this.automobileDropDown.Enabled = false;
            this.startRepairDate.Enabled = false;
            this.finishRepairDate.Enabled = false;
            this.unselectedSpareParts.Enabled = false;
            this.selectedSpareParts.Enabled = false;
            this.selectSparePartsBtn.Enabled = false;
            this.removeSparePartsBtn.Enabled = false;
            this.repairPrice.Enabled = false;
            this.CreateAutoButton.Enabled = false;
            this.repairCardDescription.Enabled = false;
            this.VinChassisTxt.Enabled = false;
            this.searchVinChassisBtn.Enabled = false;
        }

        private void SaveRepairCard(Automobile automobile, DateTime startRepairDate, string description, 
            decimal sparePartsPrice, decimal repairPrice, ListItemCollection sparePartItems)
        {
            MembershipUser currentUser = Membership.GetUser();
            RepairCard newRepairCard = new RepairCard()
            {
                Automobile = automobile,
                UserId = ((System.Guid)currentUser.ProviderUserKey),
                StartRepair = startRepairDate,
                Description = (string.IsNullOrEmpty(description) ? null : description),
                PartPrice = sparePartsPrice,
                CardPrice = repairPrice
            };
            CarServicePresentationUtility.AddSpareParts(newRepairCard, sparePartItems, this.persister);
            this.persister.CreateRepairCard(newRepairCard);
            this.persister.SaveChanges();
        }

        private void UpdateRepairCard(RepairCard repairCard, Automobile automobile, DateTime? finishRepairDate, 
            string description, decimal sparePartsPrice, decimal repairPrice, ListItemCollection sparePartItems)
        {
            repairCard.Automobile = automobile;
            repairCard.Description = (string.IsNullOrEmpty(description) ? null : description);
            repairCard.FinishRepair = finishRepairDate;
            repairCard.PartPrice = sparePartsPrice;
            repairCard.CardPrice = repairPrice;
            CarServicePresentationUtility.AddSpareParts(repairCard, sparePartItems, this.persister);
            this.persister.SaveChanges();
        }

        #endregion
    }
}