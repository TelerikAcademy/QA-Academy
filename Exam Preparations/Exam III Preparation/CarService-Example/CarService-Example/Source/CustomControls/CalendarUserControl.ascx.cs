using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentation.controls
{
    public partial class CalendarUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string SelectedDate
        {
            get
            {
                return this.selectedDateTxt.Text;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                {
                    this.selectedDateTxt.Text = value;
                }
            }
        }

        public TextBox SelectedDateTxt
        {
            get 
            {
                return this.selectedDateTxt;
            }
        }

        public bool Enabled
        {
            get
            {
                return (this.selectedDateTxt.Enabled && this.calendarBtn.Enabled);
            }
            set
            {
                this.selectedDateTxt.Enabled = value;
                this.calendarBtn.Enabled = value;
            }
        }
    }
}
