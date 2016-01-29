using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace BugTrackingSystem.Tester
{
    public partial class Tester : System.Web.UI.MasterPage
    {   
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("bg-BG");
        }
    }
}