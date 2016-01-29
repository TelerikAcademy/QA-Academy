using System;

public partial class Members_Cars_FindCar : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		this.PanelResults.Visible = this.Page.IsPostBack;
	}

	protected void ButtonSearch_Click(object sender, EventArgs e)
	{
		this.PanelResults.Visible = true;
	}

	protected void Page_PreRender(object sender, EventArgs e)
	{
		this.SqlDataSourceCars.SelectCommand =
			"SELECT * FROM Automobile WHERE Vin LIKE '%" + this.TextBoxCarVin.Text + "%'";
		this.GridViewCars.DataBind();
	}
}