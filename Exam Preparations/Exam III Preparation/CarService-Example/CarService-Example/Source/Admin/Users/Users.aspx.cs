using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using businesslogic;
using constants;
using presentation.utils;
using businesslogic.utils;

namespace presentation
{
    public partial class AdminUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                BindUsersGrid();
            }
        }

        protected void CarServiceUsersGridView_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                object sortExpressionObj = ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR];
                object sortDirectionObj = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
                if (sortExpressionObj != null && sortDirectionObj != null)
                {
                    SortingUtility.SetSortDirectionImage(this.carServiceUsers, e.Row, sortExpressionObj.ToString(), 
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

        protected void EditUserEventHandler_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int rowIndex = e.NewEditIndex; int userNameCellIndex = 0;
            string userName = CarServicePresentationUtility.GetGridCellContent(this.carServiceUsers, rowIndex, userNameCellIndex);
            if (string.IsNullOrEmpty(userName) == false)
            {
                MembershipUser user =  Membership.GetUser(userName);
                string editUserPageUrl = "~/Admin/Users/EditUser.aspx?"
                    + CarServiceConstants.USER_ID_REQUEST_PARAM_NAME + "=" + user.ProviderUserKey.ToString();
                Response.Redirect(editUserPageUrl, false);
            }             
        }

        protected void DeactivateUserEventHandler_RowDeliting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int userNameCellIndex = 0;
            string userName = CarServicePresentationUtility.GetGridCellContent(this.carServiceUsers, rowIndex, userNameCellIndex);
            if (string.IsNullOrEmpty(userName) == false)
            {
                MembershipUser user = Membership.GetUser(userName);
                if (user != null && user.IsApproved == true)
                {
                    user.IsApproved = false;
                    Membership.UpdateUser(user);                    
                }
            }
            BindUsersGrid();
        }

        protected void UsersGridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            this.carServiceUsers.PageIndex = e.NewPageIndex;
            BindUsersGrid();
        }

        protected void UsersGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortingUtility.GetSortDirection(ViewState);
            ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR] = sortDirection;
            ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR] = e.SortExpression;
            List<CarServiceUser> users = GetUsers();
            IEnumerable<CarServiceUser> sortedUsers = SortingUtility.SortUsers(users, e.SortExpression, sortDirection);
            BindUsersGrid(sortedUsers);
        }

        private void BindUsersGrid()
        {
            object sortDirectionObj = ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
            object sortExpressionObj = ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR];
            List<CarServiceUser> users = GetUsers();
            IEnumerable<CarServiceUser> sortedUsers;
            if (sortDirectionObj != null && sortExpressionObj != null)
            {
                sortedUsers = SortingUtility.SortUsers(users, sortExpressionObj.ToString(),
                    (SortDirection)sortDirectionObj);
            }
            else
            {
                ViewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR] = SortDirection.Ascending;
                ViewState[CarServiceConstants.SORT_EXPRESSION_VIEW_STATE_ATTR] = CarServiceConstants.USER_NAME_SORT_EXPRESSION;
                sortedUsers = SortingUtility.SortUsers(users, CarServiceConstants.USER_NAME_SORT_EXPRESSION,
                    SortDirection.Ascending);
            }
            BindUsersGrid(sortedUsers);
        }

        private void BindUsersGrid(IEnumerable<CarServiceUser> users)
        {
            this.carServiceUsers.DataSource = users.ToList<CarServiceUser>();
            this.carServiceUsers.DataBind();
        }

        private List<CarServiceUser> GetUsers()
        {
            List<CarServiceUser> carServiceUsers = new List<CarServiceUser>();
            MembershipUserCollection membershipUsers = Membership.GetAllUsers();
            foreach (MembershipUser currentUser in membershipUsers)
            {
                ProfileCommon profile = Profile.GetProfile(currentUser.UserName);
                if (profile != null)
                {
                    carServiceUsers.Add(new CarServiceUser(currentUser, profile));
                }
            }
            return carServiceUsers;
        }
    }
}
