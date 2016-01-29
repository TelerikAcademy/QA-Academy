using System;
using System.Web.Security;
using Data;

namespace BugTrackingSystem.UserControls
{
    public partial class UserProfile : System.Web.UI.UserControl
    {
		private Data.Tester _User;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
               BindInfo();
            }
        }

        private void BindInfo() 
        {
            lblName.Text = Server.HtmlEncode(User.Name + " " + User.Surname);
            lblRole.Text = (Roles.GetRolesForUser()[0] == "Tester" ? "Тестер" : "Администратор");
            lblLastActivity.Text ="Последно влизане :" + "<br/>" + 
				Membership.GetUser().LastActivityDate.ToShortDateString();
            if (User.LastAction != null) 
            {
                lblLastAction.Text = "Последно действие : "+ "<br/>" + Server.HtmlEncode(User.LastAction);
            }
        }

		public Data.Tester User
        {
            get 
            {
                if (_User == null) 
                {
                    _User = TestersDBManager.GetTesterByUsername(Membership.GetUser().UserName);
                }

                return _User;
            }
        }
    }
}