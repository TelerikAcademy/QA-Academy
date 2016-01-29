using System;
using System.Web.Security;

namespace BugTrackingSystem
{
    public partial class Default : System.Web.UI.Page
    {   
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_LoggedIn(object sender, EventArgs e) 
        {
            if (Roles.IsUserInRole(Login.UserName, "Administrator")) 
            {
               Response.Redirect("Administrator/Projects.aspx");
            }
            else if (Roles.IsUserInRole(Login.UserName, "Tester")) 
            {
                Response.Redirect("Tester/ProjectsBugs.aspx");
            }
        }
    }
}
