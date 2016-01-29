using System;
using System.Web.UI.WebControls;
using Data;
using System.Web.Security;

namespace BugTrackingSystem.Administrator
{
    public partial class Projects : System.Web.UI.Page
    {
        private int itemsPerPage = 10;
        private int itemsCount;

        void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjects();
                BuildPager();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Project.aspx");
        }

        protected void btnPrev_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Prev")
            {
                CurrentPage = CurrentPage - 1;
                BindProjects();
                BuildPager();
            }
        }

        protected void btnNext_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Next")
            {
                CurrentPage = CurrentPage + 1;
                BindProjects();
                BuildPager();
            }
        }

        protected void lvProjects_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditProject":
                    int projectId = -1;
                    if (Int32.TryParse(e.CommandArgument.ToString(), out projectId))
                    {
                        Response.Redirect("Project.aspx?projectId=" + projectId.ToString());
                    }
                    break;
                case "DeleteProject":
                    projectId = -1;
                    if (Int32.TryParse(e.CommandArgument.ToString(), out projectId))
                    {
                        ProjectsDBManager.Delete(projectId);
						Data.Tester tester = TestersDBManager.GetTesterByUsername(Membership.GetUser().UserName);
                        tester.LastAction = "Изтриване на проект №" + projectId;
                        TestersDBManager.Update(tester);
                        BindProjects();
                        BuildPager();
                    }
                    break;
                case "SortNumber":
                    BindColumsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortNumber";
                        BindProjects();
                        LinkButton btnNumber = (LinkButton)lvProjects.FindControl("btnNumber");
                        if (btnNumber != null)
                        {
                            btnNumber.CommandArgument = "DESC";
                            btnNumber.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortNumber";
                        BindProjects();
                        LinkButton btnNumber = (LinkButton)lvProjects.FindControl("btnNumber");
                        if (btnNumber != null)
                        {
                            btnNumber.CommandArgument = "ASC";
                            btnNumber.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortName":
                    BindColumsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortName";
                        BindProjects();
                        LinkButton btnName = (LinkButton)lvProjects.FindControl("btnName");
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
                        BindProjects();
                        LinkButton btnName = (LinkButton)lvProjects.FindControl("btnName");
                        if (btnName != null)
                        {
                            btnName.CommandArgument = "ASC";
                            btnName.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortDescription":
                    BindColumsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortDescription";
                        BindProjects();
                        LinkButton btnDescription = (LinkButton)lvProjects.FindControl("btnDescription");
                        if (btnDescription != null)
                        {
                            btnDescription.CommandArgument = "DESC";
                            btnDescription.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortDescription";
                        BindProjects();
                        LinkButton btnDescription = (LinkButton)lvProjects.FindControl("btnDescription");
                        if (btnDescription != null)
                        {
                            btnDescription.CommandArgument = "ASC";
                            btnDescription.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
            }
        }

        private void BindProjects()
        {
            lvProjects.DataSource = ProjectsDBManager.GetProjects(CurrentPage-1, itemsPerPage, Asc, SortExpression,  out itemsCount);
            lvProjects.DataBind();
        }

        private void BindColumsNames() 
        {
            LinkButton btnNumber = (LinkButton)lvProjects.FindControl("btnNumber");
            btnNumber.Text = "Номер";
            LinkButton btnName = (LinkButton)lvProjects.FindControl("btnName");
            btnName.Text = "Име";
            LinkButton btnDescription = (LinkButton)lvProjects.FindControl("btnDescription");
            btnDescription.Text = "Описание";
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