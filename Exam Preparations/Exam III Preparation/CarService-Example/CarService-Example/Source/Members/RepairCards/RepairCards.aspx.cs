using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using persistence;
using System.Data.Objects;
using presentation.utils;
using constants;
using System.Globalization;
using businesslogic.utils;
using System.Data;
using businesslogic;
using System.Web.Security;

namespace presentation
{
    public partial class MembersRepairCards : System.Web.UI.Page
    {
        private ICarServicePersister persister;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (persister == null)
            {
                this.persister = new CarServicePersister();
            }
            if (IsPostBack == false)
            {
                BindRepairCardsGrid();
                CarServiceUtility.ClearSessionAttributes(Session);
                this.notificationMsgList.CssClass = CarServiceConstants.NEGATIVE_CSS_CLASS_NAME;
            }
        }

        protected void RepairCardsGridView_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            DataControlRowType rowType = e.Row.RowType;
            if (rowType == DataControlRowType.Header)
            {
                object sortExpressionObj = ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR];
                object sortDirectionObj = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
                if (sortExpressionObj != null && sortDirectionObj != null)
                {
                    SortingUtility.SetSortDirectionImage(this.repairCardsGrid, e.Row, sortExpressionObj.ToString(),
                        ((SortDirection)sortDirectionObj));
                }
            }
            else if (rowType == DataControlRowType.DataRow)
            {
                object userIdObject = DataBinder.Eval(e.Row.DataItem, CarServiceConstants.USER_ID_SORT_EXPRESSION);
                if (userIdObject != null)
                {
                    MembershipUser currentUser = Membership.GetUser();
                    string userId = userIdObject.ToString();
                    if (string.IsNullOrEmpty(userId) == false && 
                        userId.Equals(currentUser.ProviderUserKey.ToString()))
                    {
                        e.Row.CssClass = CarServiceConstants.ACTIVE_CSS_CLASS_NAME;
                    }                        
                }
            }
        }

        protected void EditRepairCardEventHandler_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int rowIndex = e.NewEditIndex;

            string repairCardId = CarServicePresentationUtility.GetGridCellContent(this.repairCardsGrid, rowIndex, 0);
            if (string.IsNullOrEmpty(repairCardId) == false)
            {
                Session[CarServiceConstants.REPAIR_CARD_ID_PARAM_NAME] = repairCardId;
                string editRepairCardPageUrl = "~/Members/RepairCards/AddRepairCard.aspx";
                Response.Redirect(editRepairCardPageUrl);
            }
        }

        protected void RepairCardsGridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            this.repairCardsGrid.PageIndex = e.NewPageIndex;
            object repairCardsFilterObject = Session[CarServiceConstants.REPAIR_CARDS_FILTER_SESSION_ATTR_NAME];
            IQueryable<RepairCard> repairCards;
            if (repairCardsFilterObject != null)
            {
                RepairCardFilter filter = (RepairCardFilter)repairCardsFilterObject;
                repairCards = FilterRepairCards(filter);
            }
            else
            {
                repairCards = this.persister.GetRepairCards();
            }
            object sortDirectionObj = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
            object sortExpressionObj = ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR];
            if (sortDirectionObj != null && sortExpressionObj != null)
            {
                repairCards = SortingUtility.SortRepairCards(repairCards, 
                    sortExpressionObj.ToString(), (SortDirection)sortDirectionObj);
            }
            BindRepairCardsGrid(repairCards);
            CarServicePresentationUtility.ClearNotificationMsgList(this.notificationMsgList);
            CarServicePresentationUtility.HideNotificationMsgList(this.notificationMsgList);
        }

        protected void FilterRepairCards_OnClick(object sender, EventArgs e)
        {
            CarServicePresentationUtility.ClearNotificationMsgList(this.notificationMsgList);
            CarServicePresentationUtility.HideNotificationMsgList(this.notificationMsgList);
            int filterType = this.repairCardsFilterType.SelectedIndex;
            RepairCardFilter filter = new RepairCardFilter(filterType);
            if (filterType == CarServiceConstants.ALL_REPAIR_CARDS_FILTER_TYPE)
            {
                filter.VinChassis = this.VinChassisAllRepairCardsTxt.Text;
            }
            else if (filterType == CarServiceConstants.FINISHED_REPAIR_CARDS_FILTER_TYPE)
            {
                DateTime? fromFinishRepairDate = null;
                bool validFromFinishRepairDate = false;
                string fromFinishDateTxt = this.fromFinishRepairDate.SelectedDate;
                if (string.IsNullOrEmpty(fromFinishDateTxt) == false)
                {
                    DateTime fromFinishRepairDateValue = DateTime.Now;
                    validFromFinishRepairDate = CarServiceUtility.IsValidDate(fromFinishDateTxt, out fromFinishRepairDateValue);
                    if (validFromFinishRepairDate == true)
                    {
                        fromFinishRepairDate = fromFinishRepairDateValue;
                    }
                }
                DateTime? toFinishRepairDate = null;
                bool validToFinishRepairDate = false;
                string toFinishRepairDateTxt = this.toFinishRepairDate.SelectedDate;
                if (string.IsNullOrEmpty(toFinishRepairDateTxt) == false)
                {
                    DateTime toFinishRepairDateValue = DateTime.Now;
                    validToFinishRepairDate = CarServiceUtility.IsValidDate(toFinishRepairDateTxt, out toFinishRepairDateValue);
                    if (validToFinishRepairDate == true)
                    {
                        toFinishRepairDate = toFinishRepairDateValue;
                    }
                }
                if (validFromFinishRepairDate && validToFinishRepairDate)
                {
                    filter.FromFinishRepair = fromFinishRepairDate.Value;
                    filter.ToFinishRepair = toFinishRepairDate.Value;
                }
                else
                {
                    if (validFromFinishRepairDate == false)
                    {
                        CarServicePresentationUtility.AppendNotificationMsg("From finish repair date is not valid format", this.notificationMsgList);
                    }
                    if (validToFinishRepairDate == false)
                    {
                        CarServicePresentationUtility.AppendNotificationMsg("To finish repair date is not valid format", this.notificationMsgList);
                    }
                    CarServicePresentationUtility.ShowNotificationMsgList(this.notificationMsgList);
                    return;
                }
            }
            else if (filterType == CarServiceConstants.UNFINISHED_REPAIR_CARDS_FILTER_TYPE)
            {
                bool validDate = false;
                string startRepairDateTxt = this.startRepairDate.SelectedDate;
                if (string.IsNullOrEmpty(startRepairDateTxt) == false)
                {
                    DateTime startRepairDateValue = DateTime.Now;
                    validDate = CarServiceUtility.IsValidDate(startRepairDateTxt, out startRepairDateValue);
                    if (validDate == true)
                    {
                        filter.StartRepair = startRepairDateValue;
                    }
                }
                if (validDate == false)
                {
                    CarServicePresentationUtility.AppendNotificationMsg("Start repair date is not valid format", this.notificationMsgList);
                    CarServicePresentationUtility.ShowNotificationMsgList(this.notificationMsgList);
                    return;
                }
                filter.VinChassis = this.VinChassisTxt.Text;                   
            }            
            ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR] = SortDirection.Ascending;
            ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR] = CarServiceConstants.REPAIR_CARD_ID_SORT_EXPRESSION;            
            Session[CarServiceConstants.REPAIR_CARDS_FILTER_SESSION_ATTR_NAME] = filter;
            IQueryable<RepairCard> customRepairCards = FilterRepairCards(filter);
            BindRepairCardsGrid(customRepairCards);
        }

        protected void ReportType_IndexChanged(object sender, EventArgs e)
        {
            CarServicePresentationUtility.ClearNotificationMsgList(this.notificationMsgList);
            CarServicePresentationUtility.HideNotificationMsgList(this.notificationMsgList);
            int filterType = this.repairCardsFilterType.SelectedIndex;
            string filterBtnValidationGroupName = string.Empty;
            if (filterType == CarServiceConstants.ALL_REPAIR_CARDS_FILTER_TYPE)
            {
                this.allRepairCardsFilter.Visible = true;
                this.finishedRepairCardsFilter.Visible = false;
                this.unfinishedRepairCardsFilter.Visible = false;
                filterBtnValidationGroupName = "AllRepairCardsFilterValidationGroup";
            }
            else if (filterType == CarServiceConstants.FINISHED_REPAIR_CARDS_FILTER_TYPE)
            {
                this.finishedRepairCardsFilter.Visible = true;
                this.allRepairCardsFilter.Visible = false;
                this.unfinishedRepairCardsFilter.Visible = false;
                filterBtnValidationGroupName = "UnfinishedRepairCardsFilterValidationGroup";
            }
            else if (filterType == CarServiceConstants.UNFINISHED_REPAIR_CARDS_FILTER_TYPE)
            {
                this.unfinishedRepairCardsFilter.Visible = true;
                this.finishedRepairCardsFilter.Visible = false;
                this.allRepairCardsFilter.Visible = false;
                filterBtnValidationGroupName = "FinishedRepairCardsFilterValidationGroup";
            }
            this.filterButton.ValidationGroup = filterBtnValidationGroupName;
        }

        protected void RepairCardsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection newSortDirection = SortDirection.Descending;
            object currentSortDirectionObject = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
            if (currentSortDirectionObject != null)
            {
                SortDirection currentSortDirection = (SortDirection)currentSortDirectionObject;
                newSortDirection = (currentSortDirection.Equals(SortDirection.Ascending)) ? SortDirection.Descending : SortDirection.Ascending;
            }
            ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR] = newSortDirection;
            ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR] = e.SortExpression;
            object repairCardsFilterObj = Session[CarServiceConstants.REPAIR_CARDS_FILTER_SESSION_ATTR_NAME];
            IQueryable<RepairCard> repairCards;
            if (repairCardsFilterObj != null)
            {
                RepairCardFilter filter = (RepairCardFilter)repairCardsFilterObj;
                repairCards = FilterRepairCards(filter);
            }
            else
            {
                repairCards = this.persister.GetRepairCards();
            }
            IQueryable<RepairCard> sortedCards =
                SortingUtility.SortRepairCards(repairCards, e.SortExpression, newSortDirection);
            BindRepairCardsGrid(sortedCards);
        }

        #region Private methods

        private IQueryable<RepairCard> FilterRepairCards(RepairCardFilter filter)
        {
            IQueryable<RepairCard> repairCards = null;
            int filterType = filter.Type;
            switch (filterType)
            {
                case CarServiceConstants.ALL_REPAIR_CARDS_FILTER_TYPE:
                    repairCards = FilterRepairCards(filter.VinChassis);
                    break;
                case CarServiceConstants.UNFINISHED_REPAIR_CARDS_FILTER_TYPE:
                    repairCards = FilterUnfinishedRepairCards(filter.StartRepair, filter.VinChassis);
                    break;
                case CarServiceConstants.FINISHED_REPAIR_CARDS_FILTER_TYPE:
                    repairCards = FilterFinishedRepairCards(filter.FromFinishRepair, filter.ToFinishRepair);
                    break;
            }
            return repairCards;
        }


        private IQueryable<RepairCard> FilterRepairCards(string vinChassis)
        {
            IQueryable<RepairCard> repairCards;
            if (string.IsNullOrEmpty(vinChassis))
            {
                repairCards = this.persister.GetRepairCards();
            }
            else
            {
                repairCards = this.persister.GetRepairCards(vinChassis);
            }
            return repairCards;
        }

        private IQueryable<RepairCard> FilterUnfinishedRepairCards(DateTime startRepairDate, string vinChassis)
        {
            IQueryable<RepairCard> foundRepairCards = 
                this.persister.GetUnfinishedRepairCards(startRepairDate, vinChassis);
            return foundRepairCards;
        }

        private IQueryable<RepairCard> FilterFinishedRepairCards(DateTime fromFinishRepairDate, DateTime toFinishRepairDate)
        {
            IQueryable<RepairCard> foundRepairCards = 
                this.persister.GetFinishedRepairCards(fromFinishRepairDate, toFinishRepairDate);
            return foundRepairCards;
        }

        private object GetRepairCardsFormatForGrid(IQueryable<RepairCard> repairCards)
        {            
            var customRepairCards =
                from repairCard in repairCards
                select new
                {
                    repairCard.CardId,
                    repairCard.UserId,
                    repairCard.Automobile.Vin,
                    repairCard.Automobile.ChassisNumber,
                    repairCard.StartRepair,
                    repairCard.FinishRepair,
                    repairCard.CardPrice
                };
            return customRepairCards;
        }

        private void BindRepairCardsGrid()
        {
            object sortDirectionObj = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
            object sortExpressionObj = ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR];
            IQueryable<RepairCard> repairCards = this.persister.GetRepairCards();            
            if (sortDirectionObj != null && sortExpressionObj != null)
            {
                repairCards = SortingUtility.SortRepairCards(repairCards, sortExpressionObj.ToString(),
                    (SortDirection)sortDirectionObj);
            }
            else
            {
                ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR] = SortDirection.Ascending;
                ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR] = CarServiceConstants.REPAIR_CARD_ID_SORT_EXPRESSION;
            }
            BindRepairCardsGrid(repairCards);
        }

        private void BindRepairCardsGrid(IQueryable<RepairCard> repairCards)
        {
            this.repairCardsGrid.DataSource = GetRepairCardsFormatForGrid(repairCards); ;
            this.repairCardsGrid.DataBind();
        }

        #endregion
    }
}