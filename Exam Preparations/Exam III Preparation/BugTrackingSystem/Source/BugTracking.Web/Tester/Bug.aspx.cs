using System;
using System.Collections.Generic;
using System.Web.Security;
using Data;
using System.Linq;

namespace BugTrackingSystem.Tester
{ 
    public partial class Bug : System.Web.UI.Page
    {
		private Data.Bug _OldBug;
		private Data.Bug _NewBug;

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
                    ddlProjects.Items.FindByValue(Page.Request.QueryString["projectId"].ToString()).Selected = true;
                }

                var priorityList = Enum.GetValues(typeof(Priority)).Cast<Priority>();

                List<EnumValues> priorityData = new List<EnumValues>();

                foreach (var l in priorityList) 
                {
                    EnumValues v = new EnumValues(EnumStringValue.StringValue((Priority)Enum.Parse(typeof(Priority), l.ToString())),l.ToString());
                    priorityData.Add(v);
                }

                ddlPriority.DataSource = priorityData;
                ddlPriority.DataTextField = "Name";
                ddlPriority.DataValueField = "Value";
                ddlPriority.DataBind();

                var statusList = Enum.GetValues(typeof(Status)).Cast<Status>();

                List<EnumValues> statusData = new List<EnumValues>();

                foreach (var l in statusList)
                {
                    EnumValues v = new EnumValues(EnumStringValue.StringValue((Status)Enum.Parse(typeof(Status), l.ToString())), l.ToString());
                    statusData.Add(v);
                }

                ddlStatus.DataSource = statusData;
                ddlStatus.DataTextField = "Name";
                ddlStatus.DataValueField = "Value";
                ddlStatus.DataBind();

                header.InnerHtml = "Нова грешка";
                if (OldBug != null)
                {
                    header.InnerHtml = "Редакция на грешка";
                    LoadContent();
                }
            }
        }

		protected Data.Bug OldBug
        {
            get
            {
                if (_OldBug == null)
                {
                    if (Page.Request.QueryString["bugId"] != null)
                    {
                        _OldBug = BugsDBManager.GetBugByBugId(int.Parse(Page.Request.QueryString["bugId"]));
                    }
                }

                return _OldBug;
            }
        }

		protected Data.Bug NewBug
        {
            get
            {
                if (_NewBug == null)
                {
                    _NewBug = new Data.Bug();
                    if (OldBug != null)
                    {
                        _NewBug.BugId = OldBug.BugId;
                    }
                    _NewBug.CreationDate = DateTime.Now;
                    _NewBug.TesterId = TestersDBManager.GetTesterByUsername(Membership.GetUser().UserName).TesterId;
                    _NewBug.Description = txtDescription.Text;
                    _NewBug.Priority = ddlPriority.SelectedValue;
                    if (OldBug == null)
                    {
                        _NewBug.ProjectId = Int32.Parse(ddlProjects.SelectedValue);
                    }
                    else 
                    {
                        _NewBug.ProjectId = OldBug.ProjectId;
                    }
                    _NewBug.Status = ddlStatus.SelectedValue;
                }

                return _NewBug;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
				Data.Tester tester = TestersDBManager.GetTesterByUsername(Membership.GetUser().UserName);
                // insert new bug in database
                if (OldBug == null)
                {
                    tester.LastAction = "Създаване на грешка №" + BugsDBManager.Insert(NewBug);
                }
                // update existing one
                else
                {
                    BugsDBManager.Update(NewBug);
                    tester.LastAction = "Редакция на грешка №" + NewBug.BugId;
                }
                TestersDBManager.Update(tester);

                Response.Redirect("ProjectsBugs.aspx?projectId=" + NewBug.ProjectId);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) 
        {
            if (Page.Request.QueryString["projectId"] != null) 
            {
                Response.Redirect("ProjectsBugs.aspx?projectId=" + Page.Request.QueryString["projectId"]);
            }
            else if(OldBug != null)
            {
                Response.Redirect("ProjectsBugs.aspx?projectId=" + OldBug.ProjectId);
            }
            Response.Redirect("ProjectsBugs.aspx");
        }

        private void LoadContent()
        {
            trDate.Visible = true;
            lblDateValue.Text = OldBug.CreationDate.ToString();
            trOwner.Visible = true;
            if (OldBug.Tester != null)
            {
                lblOwnerValue.Text = OldBug.Tester.Name + " " + OldBug.Tester.Surname;
            }
            txtDescription.Text = OldBug.Description;
            ddlPriority.Items.FindByValue(OldBug.Priority).Selected = true;
            ddlProjects.Visible = false;
            lblProjectValue.Text = OldBug.Project.Name;
            trSattus.Visible = true;
            ddlStatus.Items.FindByValue(OldBug.Status).Selected = true;
        }
    }
}