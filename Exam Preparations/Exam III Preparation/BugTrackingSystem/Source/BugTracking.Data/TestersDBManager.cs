using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Web;
using System.Data;

namespace Data
{   

    public static class TestersDBManager
    {
        public static Tester GetTesterByTesterId(int testerId) 
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            return  db.Testers.Single(t => t.TesterId == testerId);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        }

        public static Tester GetTesterByUsername(string username)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            IQueryable<Tester> testers = from t in db.Testers
                                           where t.Username == username
                                           select t;


            foreach (Tester tester in testers)
            {
                return tester;
            }
  
            return null;
        }

        public static IEnumerable<Tester> GetTesters(int page, int pageSize, bool asc, string sortExpression, out int itemsCount) 
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            int items;
            switch (sortExpression)
            {
                case "SortNumber":
                    var testers = db.Testers.Page(page, pageSize, p => p.TesterId, p => p.Username != "admin", asc, out items);
                    itemsCount = items;
                    return testers;
                case "SortName":
                    testers = db.Testers.Page(page, pageSize, p => p.Name, p => p.Username != "admin", asc, out items);
                    itemsCount = items;
                    return testers;
                case "SortSurname":
                    testers = db.Testers.Page(page, pageSize, p => p.Surname, p => p.Username != "admin", asc, out items);
                    itemsCount = items;
                    return testers;
            }

            itemsCount = 0;
            return null;   
        }

        public static IEnumerable<Tester> GetAllTesters() 
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            return db.Testers.Where( p => p.Username != "admin");
        }

        public static int Insert(Tester tester) 
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();
            
            db.Testers.InsertOnSubmit(tester);
            db.SubmitChanges();

            return tester.TesterId;

        }

        public static void Update(Tester tester) 
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            Tester testerToUpdate = db.Testers.Single(t => t.TesterId == tester.TesterId);
           
            testerToUpdate.Password = tester.Password;
            testerToUpdate.Name = tester.Name;
            testerToUpdate.Surname = tester.Surname;
            testerToUpdate.Phone = tester.Phone;
            testerToUpdate.Email = tester.Email;
            testerToUpdate.LastAction = tester.LastAction;
            db.SubmitChanges();
        }

        public static void Delete(int testerId) 
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            // all testers bugs must leave without author
            foreach (Bug bug in db.Bugs.Where( p=> p.TesterId == testerId))
            { 
                bug.TesterId = null;
            }
           
            Tester testerToDelete = db.Testers.Single(t => t.TesterId == testerId);
            db.Testers.DeleteOnSubmit(testerToDelete);
            db.SubmitChanges();
        }

        public static int NumberFoundBugs(int testerId) 
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            return (from bug in db.Bugs
                    where bug.TesterId == testerId
                    select bug).Count();
        }

        public static int NumberParticipatingProjects(int testerId) 
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            return  (from bug in db.Bugs
                     where bug.TesterId == testerId
                     select bug.ProjectId).Distinct().Count();
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
}
                                                                                                                                                                                                                                                                                                        