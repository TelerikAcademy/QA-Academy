using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using persistence;
using businesslogic.utils;
using System.Data.Objects.DataClasses;
using System.Web.UI;
using constants;

namespace presentation.utils
{
    /// <summary>
    /// Summary description for CarServicePresentationUtility
    /// </summary>
    public class CarServicePresentationUtility
    {
        public CarServicePresentationUtility()
        {

        }

        public static string GetGridCellContent(GridView gridView, int rowIndex, int columnIndex)
        {
            GridViewRow selectedRow = gridView.Rows[rowIndex];
            if (selectedRow.RowType == DataControlRowType.DataRow)
            {
                TableCell selectedCell = selectedRow.Cells[columnIndex];
                if (selectedCell != null)
                {
                    string cellContent = selectedCell.Text;
                    return cellContent;
                }
            }
            return string.Empty;
        }

        public static void ShowNotificationMsgList(BulletedList notificationMsgList)
        {
            if (notificationMsgList.Items.Count > 0)
                notificationMsgList.Visible = true;
        }

        public static void HideNotificationMsgList(BulletedList notificationMsgList)
        {
            notificationMsgList.Visible = false;
        }

        public static void ClearNotificationMsgList(BulletedList notificationMsgList)
        {
            if (notificationMsgList.Items.Count > 0)
            {
                notificationMsgList.Items.Clear();
            }
        }

        public static void AppendNotificationMsg(string notificationMsg, BulletedList notificationMsgList)
        {
            if (string.IsNullOrEmpty(notificationMsg) == false)
            {
                ListItem item = new ListItem(notificationMsg);
                notificationMsgList.Items.Add(item);
            }
        }

        #region Repair card specific

        public static void AddSpareParts(RepairCard repairCard, ListItemCollection selectedSparePartItems, 
            ICarServicePersister persister)
        {
            repairCard.SpareParts.Clear();
            foreach (ListItem item in selectedSparePartItems)
            {
                int sparePartId;
                if (Int32.TryParse(item.Value, out sparePartId))
                {
                    SparePart sparePart = persister.GetSparePartById(sparePartId);
                    if (sparePart != null)
                    {
                        repairCard.SpareParts.Add(sparePart);
                    }
                }
            }
        }

        public static bool ProcessStartRepairDate(string startRepairDateTxt, BulletedList notificationMsgList,
            out DateTime? startRepairDate)
        {
            startRepairDate = null;
            bool validStartRepairDate = string.IsNullOrEmpty(startRepairDateTxt) == false;
            if (validStartRepairDate == false)
            {
                CarServicePresentationUtility.AppendNotificationMsg("Start repair date is required", notificationMsgList);
            }
            else
            {
                DateTime startRepairDateValue = DateTime.Now;
                validStartRepairDate = CarServiceUtility.IsValidDate(startRepairDateTxt, out startRepairDateValue);
                if (validStartRepairDate == true)
                {
                    startRepairDate = startRepairDateValue;
                }
                else
                {
                    CarServicePresentationUtility.AppendNotificationMsg("Start repair date is not in valid format", notificationMsgList);
                }
            }
            return validStartRepairDate;
        }

        public static bool ProcessFinishRepairDate(string repairDateTxt, BulletedList notificationMsgList,
            out DateTime? repairDate)
        {
            repairDate = null;
            bool validRepairDate = true;
            if (string.IsNullOrEmpty(repairDateTxt) == false)
            {
                DateTime startRepairDateValue = DateTime.Now;
                validRepairDate = CarServiceUtility.IsValidDate(repairDateTxt, out startRepairDateValue);
                if (validRepairDate == true)
                {
                    repairDate = startRepairDateValue;
                }
                else
                {
                    CarServicePresentationUtility.AppendNotificationMsg("Finish repair date is not in valid format", notificationMsgList);
                }
            }
            return validRepairDate;
        }

        public static bool ProcessRepairPrices(string sparePartsPriceTxt, string repairPriceTxt,
            BulletedList notificationMsgList, out decimal sparePartsPrice, out decimal repairPrice)
        {
            sparePartsPrice = 0M;
            repairPrice = 0M;
            bool validPrices = ValidatePrices(sparePartsPriceTxt, repairPriceTxt,
                out sparePartsPrice, out repairPrice);
            if (validPrices == false)
            {
                CarServicePresentationUtility.AppendNotificationMsg("Repair price should be larger than or equal to spare parts price", notificationMsgList);
            }
            return validPrices;
        }

        private static bool ValidatePrices(string sparePartsPriceTxt, string repairPriceTxt, out decimal sparePartsPrice, out decimal repairPrice)
        {
            bool validSparePartsPrice = false;
            bool validRepairPrice = false;
            sparePartsPrice = 0M;
            repairPrice = 0M;
            validSparePartsPrice = Decimal.TryParse(sparePartsPriceTxt, out sparePartsPrice);
            validRepairPrice = Decimal.TryParse(repairPriceTxt, out repairPrice);
            if (validSparePartsPrice == true && validRepairPrice == true)
            {
                validRepairPrice = (repairPrice >= sparePartsPrice);

            }
            return validSparePartsPrice && validRepairPrice;
        }

        #endregion

        #region Spare parts specific

        public static bool IsSparePartItemsValid(ListItemCollection selectedSparePartItems,
            BulletedList notificationMsgList)
        {
            bool validSpareParts = (selectedSparePartItems.Count > 0);
            if (validSpareParts == false)
            {
                CarServicePresentationUtility.AppendNotificationMsg("Spare parts are not selected", notificationMsgList);
            }
            return validSpareParts;
        }

        public static object GetSparePartsFormatForListBox(IQueryable<SparePart> spareParts)
        {
            var customSpareParts =
                from sp in spareParts
                select new
                {
                    PartId = sp.PartId,
                    PartName = sp.Name
                };
            return customSpareParts;
        }

        public static object GetSparePartsFormatForListBox(List<SparePart> spareParts)
        {
            var customSpareParts =
                from sp in spareParts
                select new
                {
                    PartId = sp.PartId,
                    PartName = sp.Name
                };
            return customSpareParts;
        }

        public static object GetSparePartsFormatForListBox(EntityCollection<SparePart> spareParts)
        {
            var customSpareParts =
                from sp in spareParts
                select new
                {
                    PartId = sp.PartId,
                    PartName = sp.Name
                };
            return customSpareParts;
        }

        public static void BindListBox(ListBox listBox, object listBoxDataSource)
        {
            listBox.DataSource = listBoxDataSource;
            listBox.DataBind();
        }

        /// <summary>
        /// Moves spare part items from source list box to destination list box.
        /// </summary>
        /// <param name="srcListBox">Source list box to be specified</param>
        /// <param name="destListBox">Destination list box to be specified</param>
        /// <param name="srcPriceCalculation">True - adds prices of selected spare parts, false - doesn't add prices of selected spare parts</param>
        /// <param name="persister">Persister to be specified</param>
        /// <param name="totalPrice">Stores total price of selected spare parts</param>
        public static void MoveListItems(ListBox srcListBox, ListBox destListBox, 
            bool srcPriceCalculation, ICarServicePersister persister, out decimal totalPrice)
        {
            totalPrice = 0M;
            int[] srcSelectedIndices = srcListBox.GetSelectedIndices();
            ListItemCollection destListItems = destListBox.Items;
            int totalNumberOfDestItems = destListItems.Count + srcSelectedIndices.Length;
            List<int> destItemValues = new List<int>(totalNumberOfDestItems);
            foreach (ListItem item in destListItems)
            {
                CarServiceUtility.AddEntityId(destItemValues, item.Value);
            }
            foreach (int selectedIndex in srcSelectedIndices)
            {
                ListItem item = srcListBox.Items[selectedIndex];
                CarServiceUtility.AddEntityId(destItemValues, item.Value);
            }
            BindSparePartsLists(destItemValues, srcListBox, destListBox, srcPriceCalculation, persister, out totalPrice);
        }

        private static void BindSparePartsLists(List<int> destItemSparePartIds, ListBox srcListBox,
            ListBox destListBox, bool srcPriceCalculation, ICarServicePersister persister, out decimal totalPrice)
        {
            totalPrice = 0M;
            IQueryable<SparePart> activeSpareParts = persister.GetActiveSpareParts();
            List<SparePart> srcSpareParts = new List<SparePart>();
            List<SparePart> destSpareParts = new List<SparePart>();
            foreach (SparePart currSP in activeSpareParts)
            {
                if (destItemSparePartIds.Contains(currSP.PartId))
                {
                    destSpareParts.Add(currSP);
                    if (srcPriceCalculation == false)
                    {
                        totalPrice += currSP.Price;
                    }
                }
                else
                {
                    srcSpareParts.Add(currSP);
                    if (srcPriceCalculation)
                    {
                        totalPrice += currSP.Price;
                    }
                }
            }
            object customSpareParts = CarServicePresentationUtility.GetSparePartsFormatForListBox(srcSpareParts);
            BindListBox(srcListBox, customSpareParts);
            customSpareParts = CarServicePresentationUtility.GetSparePartsFormatForListBox(destSpareParts);
            BindListBox(destListBox, customSpareParts);
        }
        #endregion

    }
}
