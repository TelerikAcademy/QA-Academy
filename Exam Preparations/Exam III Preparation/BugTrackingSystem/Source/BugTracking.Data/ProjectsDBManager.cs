using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Data
{   
    public static class ProjectsDBManager
    {   
        public static Project GetProjectByProjectId(int projtectId)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            return db.Projects.Single(t => t.ProjectId == projtectId);
        }

        public static Project GetProjectByName(string projtectName)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            IQueryable<Project> projects = from p in db.Projects
                          where p.Name == projtectName
                          select p;


            foreach (Project project in projects) 
            {
                return project;
            }
 
            return null;
        }

        public static IEnumerable<Project> GetProjects(int page, int pageSize, bool asc, string sortExpression, out int itemsCount)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            int items;
            if(sortExpression == "SortNumber")
            {
                 var projects = db.Projects.Page(page, pageSize, p => p.ProjectId, asc, out items);
                 itemsCount = items;
                 return projects;
            }
            else if (sortExpression == "SortName")
            {
                var projects = db.Projects.Page(page, pageSize, p => p.Name, asc, out items);
                itemsCount = items;
                return projects;
            }
            else if (sortExpression == "SortDescription") 
            {
                var projects = db.Projects.Page(page, pageSize, p => p.Description, asc, out items);
                itemsCount = items;
                return projects;
            }
            else
            {
                itemsCount = 0;
                return null;
            }
        }

		public static int GetBugsCountByProject(string projectName)
		{
			DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();
			string sqlFindBugsCountByProjectName = 
				"SELECT Count(*) FROM Bugs b JOIN Projects p ON (b.ProjectId=p.ProjectId) WHERE p.Name = '" + projectName + "' and b.Status != 'Closed'";
			int bugsCount = db.ExecuteQuery<int>(sqlFindBugsCountByProjectName).First();
			return bugsCount;
		}

		public static IEnumerable<Project> GetAllProjects() 
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();
            return db.Projects;
        }

        public static int Insert(Project project)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            db.Projects.InsertOnSubmit(project);
            db.SubmitChanges();

            return project.ProjectId;

        }

        public static void Update(Project project)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            Project projectToUpdate = db.Projects.Single(t => t.ProjectId == project.ProjectId);

            projectToUpdate.Name = project.Name;
            projectToUpdate.Description = project.Description;
            db.SubmitChanges();
        }

        public static void Delete(int projectId)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            // delete all project bugs
            foreach (Bug bug in db.Bugs.Where(p => p.ProjectId == projectId)) 
            {
                db.Bugs.DeleteOnSubmit(bug);
            }
            
            Project ProjectToDelete = db.Projects.Single(t => t.ProjectId == projectId);
            db.Projects.DeleteOnSubmit(ProjectToDelete);
            db.SubmitChanges();
        }
	}
}
