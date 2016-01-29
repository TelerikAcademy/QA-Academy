using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using persistence;
using constants;
using presentation.utils;

namespace presentation
{
    public partial class AdminAddSparePart : System.Web.UI.Page
    {
        private ICarServicePersister persister;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (persister == null)
            {
                persister = new CarServicePersister();
            }
            if (IsPostBack == false)
            {
                string partIdTxt = Request.QueryString[CarServiceConstants.SPARE_PART_ID_REQUEST_PARAM_NAME];                
                if(string.IsNullOrEmpty(partIdTxt))
                {
                    int maxPartId = this.persister.GetSparePartMaxId();
                    this.PartId.Text = (maxPartId + 1).ToString();
                }
                else
                {                    
                    int partId;
                    if (Int32.TryParse(partIdTxt, out partId))
                    {
                        this.PartId.Text = partIdTxt;
                        SparePart sparePart = this.persister.GetSparePartById(partId);
                        if (sparePart != null)
                        {
                            LoadSparePartInformation(sparePart);
                        }
                    }
                }
            }
        }

        protected void CancelPart_OnClick(object sender, EventArgs e)
        {
            string continueUrl = "~/Admin/SpareParts/SpareParts.aspx";
            Response.Redirect(continueUrl);
        }

        protected void AddPart_OnClick(object sender, EventArgs e)
        {
            string partIdTxt = this.PartId.Text;
            int partId;
            bool validIdValue = Int32.TryParse(partIdTxt, out partId);
            this.notificationMsgList.CssClass = CarServiceConstants.NEGATIVE_CSS_CLASS_NAME;
            if (validIdValue == false)
            {
                CarServicePresentationUtility.AppendNotificationMsg("ID is not valid", this.notificationMsgList);
            }
            string partPriceTxt = this.PartPrice.Text;
            decimal partPrice;
            bool validPriceValue = Decimal.TryParse(partPriceTxt, out partPrice);
            if (validPriceValue == false)
            {
                CarServicePresentationUtility.AppendNotificationMsg("Price is not valid", this.notificationMsgList);
            }
            int isPartActiveNum;
            if (validIdValue && validPriceValue
                && Int32.TryParse(this.PartActive.SelectedValue, out isPartActiveNum) == true)
            {                
                bool isPartActive = (isPartActiveNum == 1);
                string partName = this.PartName.Text;
                SaveSparePart(partId, partName, partPrice, isPartActive);
                CarServicePresentationUtility.AppendNotificationMsg("Part is saved successfully", this.notificationMsgList);
                this.notificationMsgList.CssClass = CarServiceConstants.POSITIVE_CSS_CLASS_NAME;
            }
            CarServicePresentationUtility.ShowNotificationMsgList(this.notificationMsgList);
        }

        private void LoadSparePartInformation(SparePart sparePart)
        {
            this.PartName.Text = sparePart.Name;
            this.PartPrice.Text = sparePart.Price.ToString();
            this.PartActive.SelectedValue = (sparePart.IsActive ? 1.ToString() : 0.ToString());
        }

        private void SaveSparePart(int partId, string partName, decimal partPrice, bool isPartActive)
        {
            SparePart updatedSparePart = this.persister.GetSparePartById(partId);
            if (updatedSparePart == null)
            {
                updatedSparePart = new SparePart()
                {
                    PartId = partId,
                    Name = partName,
                    Price = partPrice,
                    IsActive = isPartActive
                };
                this.persister.CreateSparePart(updatedSparePart);
            }
            else
            {
                updatedSparePart.Name = partName;
                updatedSparePart.Price = partPrice;
                updatedSparePart.IsActive = isPartActive;
            }
            this.persister.SaveChanges();
        }
    }
}