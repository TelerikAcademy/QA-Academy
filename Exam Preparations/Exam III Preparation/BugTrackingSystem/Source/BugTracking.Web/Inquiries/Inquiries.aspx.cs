using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using System.Web.Security;

namespace BugTrackingSystem.Administrator
{
    
    public class TesterInfo
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private int _ProjectsParticipating;

        public int ProjectsParticipating
        {
            get { return _ProjectsParticipating; }
            set { _ProjectsParticipating = value; }
        }
        private int _FoundBugs;

        public int FoundBugs
        {
            get { return _FoundBugs; }
            set { _FoundBugs = value; }
        }
        private DateTime? _LastActivity;

        public DateTime? LastActivity
        {
            get { return _LastActivity; }
            set { _LastActivity = value; }
        }
        private string _LastAction;

        public string LastAction
        {
            get { return _LastAction; }
            set { _LastAction = value; }
        }
    }

    public partial class Inquiries : System.Web.UI.Page
    {

        private int itemsPerPage = 15;
        private int itemsCount;

        protected void Page_PreInit(object sender, EventArgs e) 
        {
            if (Page.Request.QueryString["mode"] == "administrator") 
            {
                this.MasterPageFile = "~/Administrator/Administrator.master";
            }
            else if (Page.Request.QueryString["mode"] == "tester")
            {
                this.MasterPageFile = "~/Tester/Tester.master";
            }
            else 
            {
                throw new ArgumentException("Mode of page is not specified!");
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                BindProjects();
            }
        }

        private void BindProjects() 
        {
            ddlProjects.DataSource = ProjectsDBManager.GetAllProjects();
            ddlProjects.DataTextField = "Name";
            ddlProjects.DataValueField = "ProjectId";
            ddlProjects.DataBind();
            ddlProjects.Items.Insert(0,new ListItem("Изберете проект","-1"));
        }

        private void BindTesters() 
        {
            lvTesters.DataSource = TestersInfo.Skip((CurrentPage-1)*itemsPerPage).Take(itemsPerPage);
            lvTesters.DataBind();
            itemsCount = TestersInfo.Count();
            BuildTestersPager();
        }

        private void BindTestersColumnsNames() 
        {
            LinkButton btnName = (LinkButton)lvTesters.FindControl("btnName");
            btnName.Text = "Име";
            LinkButton btnProjects = (LinkButton)lvTesters.FindControl("btnProjects");
            btnProjects.Text = "Проекти";
            LinkButton btnFoundBugs = (LinkButton)lvTesters.FindControl("btnFoundBugs");
            btnFoundBugs.Text = "Открити грешки";
            LinkButton btnLastActivity = (LinkButton)lvTesters.FindControl("btnLastActivity");
            btnLastActivity.Text = "Последна активност";
            LinkButton btnLastAction = (LinkButton)lvTesters.FindControl("btnLastAction");
            btnLastAction.Text = "Последно действие";   
        }

        private void BindBugs() 
        {
            lvBugs.DataSource = BugsDBManager.GetBugs(CurrentPage - 1, itemsPerPage, Asc, SortExpression, out itemsCount);
            lvBugs.DataBind();
            BuildPager();
        }

        private void BindBugsColumnsNames() 
        {
            LinkButton btnDescription = (LinkButton)lvBugs.FindControl("btnDescription");
            btnDescription.Text = "Кратко описание";
            LinkButton btnPriority = (LinkButton)lvBugs.FindControl("btnPriority");
            btnPriority.Text = "Приоритет";
            LinkButton btnOwner = (LinkButton)lvBugs.FindControl("btnOwner");
            btnOwner.Text = "Собственик";
            LinkButton btnProject = (LinkButton)lvBugs.FindControl("btnProject");
            btnProject.Text = "Проект";
            LinkButton btnStatus = (LinkButton)lvBugs.FindControl("btnStatus");
            btnStatus.Text = "Статус";    
        }

        private void BindProjectBugs() 
        {
            lvBugs.DataSource = BugsDBManager.GetBugsByProjectId(Int32.Parse(ddlProjects.SelectedValue), CurrentPage - 1, itemsPerPage, Asc, SortExpression, false, out itemsCount);
            lvBugs.DataBind();
            BuildPager();
        }

        protected void btnTesters_Click(object sender, EventArgs e)
        {
            if (hdnFirstPage.Value == "1") 
            {
                hdnFirstPage.Value = "0";
                CurrentPage = 1;
                Asc = true;
                SortExpression = "SortNumber";
                // clear cache
                HttpContext context = HttpContext.Current;
                context.Cache.Remove("TestersInfo");
            }
            BindTesters();
        }

        protected void btnBugs_Click(object sender, EventArgs e) 
        {
            if (hdnFirstPage.Value == "1")
            {
                hdnFirstPage.Value = "0";
                CurrentPage = 1;
                Asc = true;
                SortExpression = "SortId";
            }
            BindBugs();
        }

        protected void btnProjects_Click(object sender, EventArgs e) 
        {
            BindProjects();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorsInProjects", "ShowArea('dropdown');", true);
        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e) 
        {
            if (ddlProjects.SelectedValue != "-1")
            {
                CurrentPage = 1;
                Asc = true;
                SortExpression = "SortId";
                BindProjectBugs();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorsInProjects", "ShowHideArea('dropdown');ShowArea('bugs');", true);
				this.lblBugsCount.Text = "Брой активни грешки: " + 
					ProjectsDBManager.GetBugsCountByProject(this.ddlProjects.SelectedItem.Text);
            }
            else 
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorsInProjects", "ShowHideArea('dropdown');", true);
            }
        }

        protected void lvBugs_ItemCommand(object sender, ListViewCommandEventArgs e) 
        {
            switch (e.CommandName) 
            { 
                case "SortDescription":
                    BindBugsColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortDescription";
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
                        LinkButton btnDescription = (LinkButton)lvBugs.FindControl("btnDescription");
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
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
                        LinkButton btnDescription = (LinkButton)lvBugs.FindControl("btnDescription");
                        if (btnDescription != null)
                        {
                            btnDescription.CommandArgument = "ASC";
                            btnDescription.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortPriority":
                    BindBugsColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortPriority";
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
                        LinkButton bntPriority = (LinkButton)lvBugs.FindControl("btnPriority");
                        if (bntPriority != null)
                        {
                            bntPriority.CommandArgument = "DESC";
                            bntPriority.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortPriority";
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
                        LinkButton bntPriority = (LinkButton)lvBugs.FindControl("btnPriority");
                        if (bntPriority != null)
                        {
                            bntPriority.CommandArgument = "ASC";
                            bntPriority.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortOwner":
                    BindBugsColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortOwner";
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
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
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
                        LinkButton btnOwner = (LinkButton)lvBugs.FindControl("btnOwner");
                        if (btnOwner != null)
                        {
                            btnOwner.CommandArgument = "ASC";
                            btnOwner.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortProject":
                    BindBugsColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortProject";
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
                        LinkButton btnProject = (LinkButton)lvBugs.FindControl("btnProject");
                        if (btnProject != null)
                        {
                            btnProject.CommandArgument = "DESC";
                            btnProject.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortProject";
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
                        LinkButton btnProject = (LinkButton)lvBugs.FindControl("btnProject");
                        if (btnProject != null)
                        {
                            btnProject.CommandArgument = "ASC";
                            btnProject.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortStatus":
                    BindBugsColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        Asc = true;
                        SortExpression = "SortStatus";
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
                        LinkButton btnStatus = (LinkButton)lvBugs.FindControl("btnStatus");
                        if (btnStatus != null)
                        {
                            btnStatus.CommandArgument = "DESC";
                            btnStatus.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        Asc = false;
                        SortExpression = "SortStatus";
                        if (hdnTab.Value == "projectBugs")
                        {
                            BindProjectBugs();
                        }
                        else
                        {
                            BindBugs();
                        }
                        LinkButton btnStatus = (LinkButton)lvBugs.FindControl("btnStatus");
                        if (btnStatus != null)
                        {
                            btnStatus.CommandArgument = "ASC";
                            btnStatus.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
            }
        }

        protected void lvTesters_ItemCommand(object sender, ListViewCommandEventArgs e) 
        {
            switch (e.CommandName) 
            {
                case "SortName":
                    BindTestersColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        TestersInfo = TestersInfo.OrderBy(p => p.Name).ToList();
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
                        TestersInfo = TestersInfo.OrderByDescending(p => p.Name).ToList();
                        BindTesters();
                        LinkButton btnName = (LinkButton)lvTesters.FindControl("btnName");
                        if (btnName != null)
                        {
                            btnName.CommandArgument = "ASC";
                            btnName.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortProjectsParticipating":
                    BindTestersColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        TestersInfo = TestersInfo.OrderBy(p => p.ProjectsParticipating).ToList();
                        BindTesters();
                        LinkButton btnProjects = (LinkButton)lvTesters.FindControl("btnProjects");
                        if (btnProjects != null)
                        {
                            btnProjects.CommandArgument = "DESC";
                            btnProjects.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        TestersInfo = TestersInfo.OrderByDescending(p => p.ProjectsParticipating).ToList();
                        BindTesters();
                        LinkButton btnProjects = (LinkButton)lvTesters.FindControl("btnProjects");
                        if (btnProjects != null)
                        {
                            btnProjects.CommandArgument = "ASC";
                            btnProjects.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortFoundBugs":
                    BindTestersColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        TestersInfo = TestersInfo.OrderBy(p => p.FoundBugs).ToList();
                        BindTesters();
                        LinkButton btnFoundBugs = (LinkButton)lvTesters.FindControl("btnFoundBugs");
                        if (btnFoundBugs != null)
                        {
                            btnFoundBugs.CommandArgument = "DESC";
                            btnFoundBugs.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        TestersInfo = TestersInfo.OrderByDescending(p => p.FoundBugs).ToList();
                        BindTesters();
                        LinkButton btnFoundBugs = (LinkButton)lvTesters.FindControl("btnFoundBugs");
                        if (btnFoundBugs != null)
                        {
                            btnFoundBugs.CommandArgument = "ASC";
                            btnFoundBugs.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortLastActivity":
                    BindTestersColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        TestersInfo = TestersInfo.OrderBy(p => p.LastActivity).ToList();
                        BindTesters();
                        LinkButton btnLastActivity = (LinkButton)lvTesters.FindControl("btnLastActivity");
                        if (btnLastActivity != null)
                        {
                            btnLastActivity.CommandArgument = "DESC";
                            btnLastActivity.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        TestersInfo = TestersInfo.OrderByDescending(p => p.LastActivity).ToList();
                        BindTesters();
                        LinkButton btnLastActivity = (LinkButton)lvTesters.FindControl("btnLastActivity");
                        if (btnLastActivity != null)
                        {
                            btnLastActivity.CommandArgument = "ASC";
                            btnLastActivity.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
                case "SortLastAction":
                    BindTestersColumnsNames();
                    if (e.CommandArgument.ToString() == "ASC")
                    {
                        TestersInfo = TestersInfo.OrderBy(p => p.FoundBugs).ToList();
                        BindTesters();
                        LinkButton btnLastAction = (LinkButton)lvTesters.FindControl("btnLastAction");
                        if (btnLastAction != null)
                        {
                            btnLastAction.CommandArgument = "DESC";
                            btnLastAction.Text += " <img src='../style/arrow_order_down.gif'/>";
                        }
                    }
                    else
                    {
                        TestersInfo = TestersInfo.OrderByDescending(p => p.FoundBugs).ToList();
                        BindTesters();
                        LinkButton btnLastAction = (LinkButton)lvTesters.FindControl("btnLastAction");
                        if (btnLastAction != null)
                        {
                            btnLastAction.CommandArgument = "ASC";
                            btnLastAction.Text += " <img src='../style/arrow_order_up.gif'/>";
                        }
                    }
                    break;
            }
        }

        protected void btnPrev_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Prev")
            {
                CurrentPage = CurrentPage - 1;
                if (hdnTab.Value == "projectBugs")
                {
                    BindProjectBugs();
                }
                else
                {
                    BindBugs();
                }
            }
        }

        protected void btnNext_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Next")
            {
                CurrentPage = CurrentPage + 1;
                if (hdnTab.Value == "projectBugs")
                {
                    BindProjectBugs();
                }
                else
                {
                    BindBugs();
                }
            }
        }

        protected void btnTestersPrev_Command(object sender, CommandEventArgs e) 
        {
            CurrentPage = CurrentPage - 1;
            BindTesters();
        }

        protected void btnTestersNext_Command(object sender, CommandEventArgs e) 
        {
            CurrentPage = CurrentPage + 1;
            BindTesters();
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

        private void BuildTestersPager()
        {
            if (itemsCount <= itemsPerPage)
            {
                btnTestersPrev.Visible = false;
                btnTestersNext.Visible = false;
                lblTestersCurrentPage.Visible = false;
            }
            else if (CurrentPage == 1)
            {
                btnTestersPrev.Visible = false;
                btnTestersNext.Visible = true;
                lblTestersCurrentPage.Visible = true;
            }
            else if (CurrentPage * itemsPerPage >= itemsCount)
            {
                btnTestersPrev.Visible = true;
                btnTestersNext.Visible = false;
                lblTestersCurrentPage.Visible = true;
            }
            else
            {
                btnTestersPrev.Visible = true;
                btnTestersNext.Visible = true;
                lblTestersCurrentPage.Visible = true;
            }

            lblTestersCurrentPage.Text = CurrentPage.ToString() + " от " + (itemsCount / itemsPerPage + (itemsCount % itemsPerPage > 0 ? 1 : 0));

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

        public List<TesterInfo> TestersInfo 
        {
            get 
            {
                HttpContext context = HttpContext.Current;
                List<TesterInfo> testersInfo = (List<TesterInfo>)context.Cache["TestersInfo"];
                if (testersInfo == null) 
                {
                    List<Data.Tester> testers = TestersDBManager.GetAllTesters().ToList();
                    testersInfo = new List<TesterInfo>();

                    foreach (var tester in testers)
                    {
                        TesterInfo testerInfo = new TesterInfo();
                        testerInfo.Name = tester.Name + " " + tester.Surname;
                        testerInfo.ProjectsParticipating = TestersDBManager.NumberParticipatingProjects(tester.TesterId);
                        testerInfo.FoundBugs = TestersDBManager.NumberFoundBugs(tester.TesterId);
						MembershipUser user = Membership.GetUser(tester.Username);
						if (user != null)
						{
							testerInfo.LastActivity = Membership.GetUser(tester.Username).LastActivityDate;
						}
						else
						{
							testerInfo.LastActivity = null;
						}
                        testerInfo.LastAction = tester.LastAction;

                        testersInfo.Add(testerInfo);
                    }

                    context.Cache["TestersInfo"] = testersInfo;
                }

                return testersInfo;
            }
            set 
            {
                HttpContext context = HttpContext.Current;
                context.Cache["TestersInfo"] = value;
            }
        }

		protected string SafeEval(string propertyName)
		{
			object obj = this.Eval(propertyName);
			if (obj == null)
			{
				return "";
			}
			return Server.HtmlEncode(obj.ToString());
		}
    }
}