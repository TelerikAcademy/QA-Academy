using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using presentation.utils;
using constants;
using persistence;
using System.Data.Objects;
using businesslogic.utils;

namespace presentation
{   
    public partial class AdminSpareParts : System.Web.UI.Page
    {
        private ICarServicePersister persister;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(persister == null) 
            {
                persister = new CarServicePersister();      
            }
            if (IsPostBack == false)
            {
                BindSparePartsGrid();
            }
        }

        protected void SparePartsGridView_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                object sortExpressionObj = ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR];
                object sortDirectionObj = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
                if (sortExpressionObj != null && sortDirectionObj != null)
                {
                    SortingUtility.SetSortDirectionImage(this.sparePartsGrid, e.Row, sortExpressionObj.ToString(),
                        ((SortDirection)sortDirectionObj));
                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object isApprovedObject = DataBinder.Eval(e.Row.DataItem, "IsActive");
                if (isApprovedObject != null)
                {
                    bool isApproved = (bool)isApprovedObject;
                    if (isApproved == false)
                    {
                        e.Row.CssClass = CarServiceConstants.INACTIVE_CSS_CLASS_NAME;
                    }
                }
            }
        }

        protected void EditSparePartventHandler_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int rowIndex = e.NewEditIndex;
            string partId = CarServicePresentationUtility.GetGridCellContent(this.sparePartsGrid, rowIndex, 0);
            if (string.IsNullOrEmpty(partId) == false)
            {
                string addSparePartPageUrl = "~/Admin/SpareParts/AddSparePart.aspx?"
                    + CarServiceConstants.SPARE_PART_ID_REQUEST_PARAM_NAME + "=" + partId;
                Response.Redirect(addSparePartPageUrl, false);
            }
        }

        protected void DeactivateSparePartEventHandler_RowDeliting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = e.RowIndex;
            string partId = CarServicePresentationUtility.GetGridCellContent(this.sparePartsGrid, rowIndex, 0);
            if (string.IsNullOrEmpty(partId) == false)
            {
                int partIdNum;
                if (Int32.TryParse(partId, out partIdNum) == true)
                {
                    SparePart sparePart = this.persister.GetSparePartById(partIdNum);
                    if (sparePart != null && sparePart.IsActive)
                    {
                        sparePart.IsActive = false;
                        this.persister.SaveChanges();
                    }
                }               
            }
            BindSparePartsGrid();
        }

        protected void SparePartsGridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            this.sparePartsGrid.PageIndex = e.NewPageIndex;
            BindSparePartsGrid();
        }

        protected void SparePartsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortingUtility.GetSortDirection(ViewState);
            ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR] = sortDirection;
            ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR] = e.SortExpression;
            IQueryable<SparePart> spareParts = this.persister.GetSpareParts();
            IQueryable<SparePart> sortedSpareParts = SortingUtility.SortSpareParts(spareParts, e.SortExpression, sortDirection);
            BindSparePartsGrid(sortedSpareParts);
        }

        private void BindSparePartsGrid()
        {

            object sortDirectionObj = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
            object sortExpressionObj = ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR];
            IQueryable<SparePart> spareParts = this.persister.GetSpareParts();
            IQueryable<SparePart> sortedSpareParts;
            if (sortDirectionObj != null && sortExpressionObj != null)
            {
                sortedSpareParts = SortingUtility.SortSpareParts(spareParts, sortExpressionObj.ToString(), 
                    (SortDirection)sortDirectionObj);
            }
            else
            {
                ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR] = SortDirection.Ascending;
                ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR] = CarServiceConstants.SPARE_PART_ID_SORT_EXPRESSION;
                sortedSpareParts = spareParts;
            }
            BindSparePartsGrid(sortedSpareParts);
        }

        private void BindSparePartsGrid(IEnumerable<SparePart> spareParts)
        {
            this.sparePartsGrid.DataSource = spareParts;
            this.sparePartsGrid.DataBind();
        }
    }
}