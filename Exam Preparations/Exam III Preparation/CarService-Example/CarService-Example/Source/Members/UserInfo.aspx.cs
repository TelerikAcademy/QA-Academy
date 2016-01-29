using System;
using System.Web.Security;
using System.Collections;

public partial class Members_UserInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		MembershipUser user = GetUser();
		if (user != null)
		{
			this.LiteralUserName.Text = user.UserName;
			this.LiteralEmail.Text = user.Email;
			this.EntityDataSourceRepairCards.WhereParameters["userId"].DefaultValue =
				user.ProviderUserKey.ToString();
			this.GridViewRepairCards.DataBind();
		}
	}

	private MembershipUser GetUser()
	{
		if (Request.Params["username"] != null)
		{
			MembershipUserCollection matchingUsers =
				Membership.FindUsersByName(Request.Params["username"]);
			if (matchingUsers.Count != 0)
			{
				IEnumerator usersEnumarator = matchingUsers.GetEnumerator();
				usersEnumarator.MoveNext();
				return (MembershipUser)usersEnumarator.Current;
			}
		}

		return null;
	}
}
