using System;
using System.Web.Security;

public partial class Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	protected void LinkButtonUserInfo_Click(object sender, EventArgs e)
    {
		string userName = Membership.GetUser().UserName;
		Response.Redirect("~/Members/UserInfo.aspx?username=" + userName);
    }	
}
