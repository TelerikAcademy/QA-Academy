using System;
using System.Web.UI.WebControls;
using Data;
using System.Web.Security;

namespace BugTrackingSystem.Tester
{
    public partial class ProjectsBugs : System.Web.UI.Page
    {
        private int itemsPerPage = 20;
        private int itemsCount;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlProjects.DataSource = ProjectsDBManager.GetAllProjects();
                ddlProjects.DataTextField = "Name";
                ddlProjects.DataValueField = "ProjectId";
                ddlProjects.DataBind();

                if (Page.Request.QueryString["projectId"] != null) 
                {
                    ddlProjects.Items.FindByValue(Page.Request.QueryString["projectId"]).Selected = true;
                }
                BindBugs();
                BuildPager();
            }
        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBugs();
            BuildPager();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bug.aspx?projectId="+ddlProjects.SelectedItem.Value);
        }

        protected void btnPrev_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Prev")
            {
                CurrentPage = CurrentPage - 1;
                BindBugs();
                BuildPager();
            }
        }

        protected void btnNext_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Next")
            {
                CurrentPage = CurrentPage + 1;
                BindBugs();
                BuildPager();
            }
        }

        protected void lvBugs_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditBug":
                    int bugId = -1;
                    if (Int32.TryParse(e.CommandArgument.ToString(), out bugId))
                    {
                        Response.Redirect("Bug.aspx?bugId=" + bugId.ToString());
                    }
                    break;
                case "DeleteBug":
                    bugId = -1;
                    if (Int32.TryParse(e.CommandArgument.ToString(), out bugId))
                    {
                        BugsDBManager.Delete(bugId);
                        Data.Tester tester = TestersDBManager.GetTesterByUsername(Membership.GetUser().UserName);
                        tester.LastAction = "Изтриване на грешка №" + bugId;
                        TestersDBManager.Update(tester);
                        BindBugs();
                        BuildPager();
                    }
                    break;
                case "SortId":
                    BindColumsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortId";
                        BindBugs();
                        LinkButton btnId = (LinkButton)lvBugs.FindControl("btnId");
                        if (btnId != null)
                        {
                            btnId.CommandArgument = "DESC";
                            btnId.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortId";
                        BindBugs();
                        LinkButton btnId = (LinkButton)lvBugs.FindControl("btnId");
                        if (btnId != null)
                        {
                            btnId.CommandArgument = "ASC";
                            btnId.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortOwner":
                    BindColumsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortOwner";
                        BindBugs();
                        LinkButton btnOwner = (LinkButton)lvBugs.FindControl("btnOwner");
                        if (btnOwner != null)
                        {
                            btnOwner.CommandArgument = "DESC";
                            btnOwner.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortOwner";
                        BindBugs();
                        LinkButton btnOwner = (LinkButton)lvBugs.FindControl("btnOwner");
                        if (btnOwner != null)
                        {
                            btnOwner.CommandArgument = "ASC";
                            btnOwner.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortPriority":
                    BindColumsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortPriority";
                        BindBugs();
                        LinkButton btnPriority = (LinkButton)lvBugs.FindControl("btnPriority");
                        if (btnPriority != null)
                        {
                            btnPriority.CommandArgument = "DESC";
                            btnPriority.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortPriority";
                        BindBugs();
                        LinkButton btnPriority = (LinkButton)lvBugs.FindControl("btnPriority");
                        if (btnPriority != null)
                        {
                            btnPriority.CommandArgument = "ASC";
                            btnPriority.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortDate":
                    BindColumsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortDate";
                        BindBugs();
                        LinkButton btnDate = (LinkButton)lvBugs.FindControl("btnDate");
                        if (btnDate != null)
                        {
                            btnDate.CommandArgument = "DESC";
                            btnDate.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortDate";
                        BindBugs();
                        LinkButton btnDate = (LinkButton)lvBugs.FindControl("btnDate");
                        if (btnDate != null)
                        {
                            btnDate.CommandArgument = "ASC";
                            btnDate.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
            }
        }

        private void BindBugs()
        {
			if (ddlProjects.SelectedValue != "")
			{
				lvBugs.DataSource = BugsDBManager.GetBugsByProjectId(
					Int32.Parse(ddlProjects.SelectedValue), CurrentPage - 1,
					itemsPerPage, Asc, SortExpression, true, out itemsCount);
				lvBugs.DataBind();
			}
        }

        private void BindColumsNames() 
        {
            LinkButton btnId = (LinkButton)lvBugs.FindControl("btnId");
            btnId.Text = "Номер на грешка";
            LinkButton btnOwner = (LinkButton)lvBugs.FindControl("btnOwner");
            btnOwner.Text = "Собственик";
            LinkButton btnPriority = (LinkButton)lvBugs.FindControl("btnPriority");
            btnPriority.Text = "Приоритет";
            LinkButton btnDate = (LinkButton)lvBugs.FindControl("btnDate");
            btnDate.Text = "Дата";
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
                    ViewState["SortExpression"] = "SortId";
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