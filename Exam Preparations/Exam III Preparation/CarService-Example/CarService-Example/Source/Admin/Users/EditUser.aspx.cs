using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using constants;
using System.Web.Security;
using businesslogic.utils;

namespace presentation
{
    public partial class AdminEditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                string userId = Request.QueryString[CarServiceConstants.USER_ID_REQUEST_PARAM_NAME];
                Guid userProviderKey;
                if (CarServiceUtility.GuidTryParse(userId, out userProviderKey) == true)
                {
                    MembershipUser user = Membership.GetUser(userProviderKey);
                    if (user != null)
                    {
                        this.UserName.Text = user.UserName;
                        this.Email.Text = user.Email;
                        this.UserActive.SelectedValue = (user.IsApproved ? 
                            CarServiceConstants.ACTIVE_STATUS.ToString() : CarServiceConstants.INACTIVE_STATUS.ToString());
                        ProfileCommon userProfile = Profile.GetProfile(user.UserName);
                        this.FirstName.Text = userProfile.FirstName;
                        this.LastName.Text = userProfile.LastName;
                    }
                }
            }
        }

        protected void SaveEventHandler_OnClick(Object sender, EventArgs e)
        {            
            int selectedValue;
            if (Int32.TryParse(this.UserActive.SelectedValue, out selectedValue))
            {
                bool isActive = (selectedValue == 1);
                string userName = this.UserName.Text;
                string email = this.Email.Text;
                string firstName = this.FirstName.Text;
                string lastName = this.LastName.Text;
                MembershipUser user = Membership.GetUser(userName);
                if (user != null)
                {
                    user.Email = email;
                    user.IsApproved = isActive;
                    Membership.UpdateUser(user);
                    ProfileCommon userProfile = Profile.GetProfile(userName);
                    if (userProfile != null)
                    {
                        userProfile.FirstName = firstName;
                        userProfile.LastName = lastName;
                        userProfile.Save();
                    }
                }
            }
        }

        protected void ResetPasswordEventHandler_OnClick(Object sender, EventArgs e)
        {
            string password = this.Password.Text;
            /**** Resetting user password without knowing old password ****/
            string userName = this.UserName.Text;
            if (string.IsNullOrEmpty(userName) == false && string.IsNullOrEmpty(password) == false)
            {
                MembershipUser user = Membership.GetUser(userName);
                if (user != null)
                {user.ChangePassword(user.ResetPassword(), password);}
            }
        }

        protected void CancelEventHandler_OnClick(Object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Users/Users.aspx");
        }
    }
}