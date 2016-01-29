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
using businesslogic.utils;

namespace presentation
{
    public partial class MembersCars : System.Web.UI.Page
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
                CarServiceUtility.ClearSessionAttributes(Session);
                BindAutomobilesGrid();
            }
        }

        protected void CarsGridView_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                object sortExpressionObj = ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR];
                object sortDirectionObj = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
                if (sortExpressionObj != null && sortDirectionObj != null)
                {
                    SortingUtility.SetSortDirectionImage(this.automobilesGrid, e.Row, sortExpressionObj.ToString(),
                        ((SortDirection)sortDirectionObj));
                }
            }
        }

        protected void EditAutomobileEventHandler_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int rowIndex = e.NewEditIndex;
            string autoId = CarServicePresentationUtility.GetGridCellContent(this.automobilesGrid, rowIndex, 0);
            if (string.IsNullOrEmpty(autoId) == false)
            {
                Session[CarServiceConstants.AUTOMOBILE_ID_REQUEST_PARAM_NAME] = autoId;
                string editAutomobilePageUrl = "~/Members/Cars/AddCar.aspx";
                Response.Redirect(editAutomobilePageUrl);
            }
        }

        protected void AutomobilesGridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            this.automobilesGrid.PageIndex = e.NewPageIndex;
            BindAutomobilesGrid();
        }

        protected void CarsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortingUtility.GetSortDirection(ViewState);
            ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR] = sortDirection;
            ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR] = e.SortExpression;
            IQueryable<Automobile> automobiles = this.persister.GetAutomobiles();
            IQueryable<Automobile> sortedAutomobiles = SortingUtility.SortAutomobiles(automobiles, e.SortExpression, sortDirection);
            BindAutomobilesGrid(sortedAutomobiles);
        }

        private void BindAutomobilesGrid()
        {
            object sortDirectionObj = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
            object sortExpressionObj = ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR];
            IQueryable<Automobile> automobiles = this.persister.GetAutomobiles();
            IQueryable<Automobile> sortedAutomobiles;
            if (sortDirectionObj != null && sortExpressionObj != null)
            {
                sortedAutomobiles = SortingUtility.SortAutomobiles(automobiles, sortExpressionObj.ToString(),
                    (SortDirection)sortDirectionObj);
            }
            else
            {
                ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR] = SortDirection.Ascending;
                ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR] = CarServiceConstants.AUTOMOBILE_ID_SORT_EXPRESSION;
                sortedAutomobiles = automobiles;
            }
            BindAutomobilesGrid(sortedAutomobiles);
        }

        private void BindAutomobilesGrid(IQueryable<Automobile> automobiles)
        {
            this.automobilesGrid.DataSource = automobiles;
            this.automobilesGrid.DataBind();
        }
    }
}