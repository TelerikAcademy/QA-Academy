using System;
using System.Web.UI.WebControls;
using Data;
using System.Web.Security;

namespace BugTrackingSystem.Administrator
{
    public partial class Testers : System.Web.UI.Page
    {

        private int itemsPerPage = 10;
        private int itemsCount;

        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                BindTesters();
                BuildPager();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e) 
        {
            Response.Redirect("Tester.aspx");   
        }

        protected void btnPrev_Command(object sender, CommandEventArgs e) 
        {
            if (e.CommandName == "Prev") 
            {
                CurrentPage = CurrentPage - 1;
                BindTesters();
                BuildPager();
            }
        }

        protected void btnNext_Command(object sender, CommandEventArgs e) 
        {
            if (e.CommandName == "Next") 
            {
                CurrentPage = CurrentPage + 1;
                BindTesters();
                BuildPager();
            }
        }

        protected void lvTesters_ItemCommand(object sender, ListViewCommandEventArgs e) 
        {
            switch (e.CommandName) 
            { 
                case "EditTester":
                    int testerId = -1;
                    if (Int32.TryParse(e.CommandArgument.ToString(), out testerId)) 
                    {
                        Response.Redirect("Tester.aspx?testerId="+testerId.ToString());
                    }
                    break;
                case "DeleteTester":
                    testerId = -1;
                    if (Int32.TryParse(e.CommandArgument.ToString(), out testerId)) 
                    {
                        var testerToDelete = TestersDBManager.GetTesterByTesterId(testerId);
                        Membership.DeleteUser(testerToDelete.Username);
                        Roles.RemoveUserFromRole(testerToDelete.Username, "Tester");
                        TestersDBManager.Delete(testerId);
						Data.Tester tester = TestersDBManager.GetTesterByUsername(Membership.GetUser().UserName);
                        tester.LastAction = "Изтриване на тестер №" + testerId;
                        TestersDBManager.Update(tester);
                        BindTesters();
                        BuildPager();
                    }
                    break;
                case "SortName":
                    BindColumsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortName";
                        BindTesters();
                        LinkButton btnName = (LinkButton)lvTesters.FindControl("btnName");
                        if (btnName != null) 
                        {
                            btnName.CommandArgument = "DESC";
                            btnName.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else 
                    {
                        Asc = false;
                        SortExpression = "SortName";
                        BindTesters();
                        LinkButton btnName = (LinkButton)lvTesters.FindControl("btnName");
                        if (btnName != null)
                        {
                            btnName.CommandArgument = "ASC";
                            btnName.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortSurname":
                    BindColumsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortSurname";
                        BindTesters();
                        LinkButton btnSurname = (LinkButton)lvTesters.FindControl("btnSurname");
                        if (btnSurname != null)
                        {
                            btnSurname.CommandArgument = "DESC";
                            btnSurname.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortSurname";
                        BindTesters();
                        LinkButton btnSurname = (LinkButton)lvTesters.FindControl("btnSurname");
                        if (btnSurname != null)
                        {
                            btnSurname.CommandArgument = "ASC";
                            btnSurname.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
            }
        }

        private void BindTesters()
        {
            lvTesters.DataSource = TestersDBManager.GetTesters(CurrentPage - 1, itemsPerPage, Asc, SortExpression, out itemsCount);
            lvTesters.DataBind();
           
        }

        private void BindColumsNames() 
        {
            LinkButton btnName = (LinkButton)lvTesters.FindControl("btnName");
            btnName.Text = "Име";
            LinkButton btnSurname = (LinkButton)lvTesters.FindControl("btnSurname");
            btnSurname.Text = "Фамилия";
        }

        private void BuildPager()
        {
            if (itemsCount <= itemsPerPage)
            {
                btnPrev.Visible = false;
                btnNext.Visible = false;
                lblCurrentPage.Visible = false;
            }
            else if (CurrentPage == 1)
            {
                btnPrev.Visible = false;
                btnNext.Visible = true;
                lblCurrentPage.Visible = true;
            }
            else if (CurrentPage * itemsPerPage >= itemsCount)
            {
                btnPrev.Visible = true;
                btnNext.Visible = false;
                lblCurrentPage.Visible = true;
            }
            else
            {
                btnPrev.Visible = true;
                btnNext.Visible = true;
                lblCurrentPage.Visible = true;
            }

            lblCurrentPage.Text = CurrentPage.ToString() + " от " + (itemsCount / itemsPerPage + (itemsCount % itemsPerPage > 0 ? 1 : 0));

        }

        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    ViewState["CurrentPage"] = 1;
                }
                return Int32.Parse(ViewState["CurrentPage"].ToString());
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        private bool Asc
        {
            get
            {
                if (ViewState["ASC"] == null)
                {
                    ViewState["ASC"] = true;
                }
                return bool.Parse(ViewState["ASC"].ToString());
            }
            set
            {
                ViewState["ASC"] = value;
            }
        }

        private string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = "SortNumber";
                }
                return ViewState["SortExpression"].ToString();
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }
    }
}