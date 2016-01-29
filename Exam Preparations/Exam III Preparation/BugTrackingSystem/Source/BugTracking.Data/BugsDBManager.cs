using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public static class BugsDBManager
    {
         public static Bug GetBugByBugId(int bugId)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            return db.Bugs.Single(t => t.BugId == bugId);
        }

        public static IEnumerable<Bug> GetBugs(int page, int pageSize, bool asc, string sortExpression, out int itemsCount)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            int items;
            switch (sortExpression) 
            { 
                case "SortDescription":
                    var bugs = db.Bugs.Page(page, pageSize, p => p.Description, 
                                                            p=> p.Status != Status.Closed.ToString(), 
                                                            p => p.Status != Status.Deleted.ToString(), 
                                                            asc, out items);
                    itemsCount = items;
                    return bugs;
                case "SortId":
                    bugs = db.Bugs.Page(page, pageSize, p => p.BugId,
                                                        p => p.Status != Status.Closed.ToString(),
                                                        p => p.Status != Status.Deleted.ToString(),
                                                        asc, out items);
                    itemsCount = items;
                    return bugs;
                case "SortPriority":
                    bugs = db.Bugs.Page(page, pageSize, p => p.Priority,
                                                        p => p.Status != Status.Closed.ToString(),
                                                        p => p.Status != Status.Deleted.ToString(),
                                                        asc, out items);
                    itemsCount = items;
                    return bugs;
                case "SortOwner":
                    bugs = db.Bugs.Page(page, pageSize, p => p.Tester.Name,
                                                        p => p.Status != Status.Closed.ToString(),
                                                        p => p.Status != Status.Deleted.ToString(),
                                                        asc, out items);
                    itemsCount = items;
                    return bugs;
                case "SortProject":
                    bugs = db.Bugs.Page(page, pageSize, p => p.Project.Name,
                                                        p => p.Status != Status.Closed.ToString(),
                                                        p => p.Status != Status.Deleted.ToString(),
                                                        asc, out items);
                    itemsCount = items;
                    return bugs;
                case "SortStatus":
                    bugs = db.Bugs.Page(page, pageSize, p => p.Status,
                                                        p => p.Status != Status.Closed.ToString(),
                                                        p => p.Status != Status.Deleted.ToString(),
                                                        asc, out items);
                    itemsCount = items;
                    return bugs;
            }

            itemsCount = 0;
            return null;
        }

        public static IEnumerable<Bug> GetBugsByProjectId(int projectId, int page, int pageSize, bool asc, string sortExpression, bool showClosed, out int itemsCount)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            int items;
            switch (sortExpression)
            {
                case "SortId":
                    if (showClosed)
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.BugId,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                    else
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.BugId,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Closed.ToString(),
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                case "SortOwner":
                    if (showClosed)
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.TesterId,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                    else
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.TesterId,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Closed.ToString(),
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                case "SortPriority":
                    if (showClosed)
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.Priority,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                    else
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.Priority,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Closed.ToString(),
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                case "SortDate":
                    if (showClosed)
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.CreationDate,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                    else
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.CreationDate,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Closed.ToString(),
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                case "SortDescription":
                    if (showClosed)
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.Description,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                    else
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.Description,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Closed.ToString(),
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                case "SortProject":
                    if (showClosed)
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.Project.Name,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                    else
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.Project.Name,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Closed.ToString(),
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                case "SortStatus":
                    if (showClosed)
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.Status,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
                    else
                    {
                        var bugs = db.Bugs.Page(page, pageSize, p => p.Status,
                                                                p => p.ProjectId == projectId,
                                                                p => p.Status != Status.Closed.ToString(),
                                                                p => p.Status != Status.Deleted.ToString(),
                                                                asc, out items);
                        itemsCount = items;
                        return bugs;
                    }
            }

            itemsCount = 0;
            return null;
        }

        public static int Insert(Bug bug)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            db.Bugs.InsertOnSubmit(bug);
            db.SubmitChanges();

            return bug.BugId;

        }

        public static void Update(Bug bug)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            Bug bugToUpdate = db.Bugs.Single(t => t.BugId == bug.BugId);

            bugToUpdate.Description = bug.Description;
            bugToUpdate.Priority = bug.Priority;
            bugToUpdate.Status = bug.Status;
            db.SubmitChanges();
        }

        public static void Delete(int bugId)
        {
            DataClassesBugTrackingSystemDataContext db = new DataClassesBugTrackingSystemDataContext();

            Bug bugToDelete = db.Bugs.Single(t => t.BugId == bugId);
            bugToDelete.Status = Status.Deleted.ToString();
            db.SubmitChanges();
        }
    }
}
