using System;
using System.Web.UI.WebControls;
using Data;
using System.Web.Security;

namespace BugTrackingSystem.Administrator
{
    public partial class Project : System.Web.UI.Page
    {
		private Data.Project _OldProject;
		private Data.Project _NewProject;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                header.InnerText = "Нов проект";
                if (OldProject != null)
                {
                    header.InnerText = "Редакция на проект";
                    LoadContent();
                }
            }
        }

		protected Data.Project OldProject
        {
            get
            {
                if (_OldProject == null)
                {
                    if (Page.Request.QueryString["ProjectId"] != null)
                    {
                        _OldProject = ProjectsDBManager.GetProjectByProjectId(int.Parse(Page.Request.QueryString["ProjectId"]));
                    }
                }

                return _OldProject;
            }
        }

		protected Data.Project NewProject
        {
            get
            {
                if (_NewProject == null)
                {
					_NewProject = new Data.Project();
                    if (OldProject != null) 
                    {
                        _NewProject.ProjectId = OldProject.ProjectId;
                    }
                    _NewProject.Name = txtName.Text;
                    _NewProject.Description = txtDescription.Text;
                }

                return _NewProject;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.Request.QueryString["ProjectId"] != null) 
            {
                cuvName.Enabled = false;
            }
            Page.Validate();
            if (Page.IsValid)
            {
				Data.Tester tester = TestersDBManager.GetTesterByUsername(Membership.GetUser().UserName);
                // insert new project in database
                if (OldProject == null)
                {
                    tester.LastAction = "Създаване на проект №" + ProjectsDBManager.Insert(NewProject);
                }
                // update existing one
                else
                {
                    ProjectsDBManager.Update(NewProject);
                    tester.LastAction = "Редакция на проект №" + NewProject.ProjectId;
                }
                TestersDBManager.Update(tester);

                Response.Redirect("Projects.aspx");
            }
        }

        private void LoadContent()
        {
            txtName.Text = OldProject.Name;
            txtDescription.Text = OldProject.Description;
        }

        protected void cuvName_ServerValidate(object sender, ServerValidateEventArgs e) 
        {
            if(ProjectsDBManager.GetProjectByName(txtName.Text) == null)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }
    }
}