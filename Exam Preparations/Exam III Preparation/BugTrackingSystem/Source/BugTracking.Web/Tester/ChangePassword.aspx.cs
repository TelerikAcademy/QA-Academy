using System;
using Data;
using System.Web.Security;

namespace BugTrackingSystem.Administrator
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnChangePassword_Click(object sender, EventArgs e) 
        {
			MembershipUser user = Membership.GetUser(Tester.Username);
			string genPasswrd = user.ResetPassword();
			user.ChangePassword(genPasswrd, txtNewPassword.Text);
            content.Style.Add("display", "none");
            lblMessage.Text = "Паролата е променена успешно!";
        }

        protected void btnCancel_Click(object sender, EventArgs e) 
        {
			Response.Redirect("~/Default.aspx");
		}

		protected Data.Tester Tester
        {
            get
            {
                 return TestersDBManager.GetTesterByTesterId(
					 int.Parse(Page.Request.QueryString["testerId"]));
            }
        }
    }
}