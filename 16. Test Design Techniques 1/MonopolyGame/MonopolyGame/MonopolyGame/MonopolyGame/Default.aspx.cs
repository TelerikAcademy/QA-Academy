using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

public partial class Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShapesList.DataSource = new[]
			{
				new { ID = "Option", Text = "Option", Background = "#619eea" }
			};
        ShapesList.DataBind();
    }
    protected void OnTextChanged(object sender, EventArgs e)
    {
        RadNumericTextBox players = (RadNumericTextBox)sender;
        double playerCount = (double)players.Value;
        for (int i = 2; i <= 6; i++)
        {
            string id = "PlayerName" + i;
            if (i <= Math.Truncate(playerCount))
            {
                FindControl(id).Visible = true;
            }
            else
            {
                FindControl(id).Visible = false;
            }
        }
        if (MoneyPerPlayer.Value != null)
        {
            int value = 12000 - ((int)MoneyPerPlayer.Value * (int)players.Value);
            BankMoney.Value = value;
        }
    }
    protected void PlayerMoneyChanged(object sender, EventArgs e)
    {
        RadNumericTextBox moneyPerPlayer = (RadNumericTextBox)sender;
        int value = 12000 - ((int)moneyPerPlayer.Value * (int)PlayerCount.Value);
        BankMoney.Value = value;
    }
    protected void ValidateTextBoxes(object sender, ServerValidateEventArgs args)
    {
        string strRegex = "^[a-zA-Z]+$";

        Regex regex = new Regex(strRegex);

        StringBuilder invalidTextBoxes = new StringBuilder();
        for (int i = 1; i <=6; i++)
        {
            string id = "PlayerName" + i;
            RadTextBox textbox = (RadTextBox)FindControl(id);
            if (textbox!=null)
            {
                if (!regex.IsMatch(textbox.Text) && textbox.Text != "")
                {
                    invalidTextBoxes.AppendLine(id);
	            }
            }
        }
        if (invalidTextBoxes.Length != 0)
        {
            ValidationResult.Text = String.Format("INVALID: {0}", invalidTextBoxes.ToString());
            ValidationResult.Style["Color"] = "red";
        }
        else
        {
            ValidationResult.Text = "VALID";
            ValidationResult.Style["Color"] = "green";
        }
    }
}
