using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BugTrackingSystem.Administrator
{
    public partial class Administrator : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
				Data.Tester admin = new Data.Tester();
                admin.Name ="Администратор";
               // UserProfile.User = admin;
            }
        }
    }
}