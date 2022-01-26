using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Data.Entities;
using TaskManager.DataAccess.Repository.Interface;

namespace TaskManager.DataAccess.Repository
{
    public class ProjectDataAccess : TaskManagerDataAccess<Project>, IProjectDataAccess
    {
        private readonly TaskManagerContext _db;
        public ProjectDataAccess(TaskManagerContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Project> GetProjectsWithStoredProcedure()
        {
            return _db.Projects.FromSqlRaw("EXECUTE " + StoredProcedures.GetProjects).ToList();
        }
    }
}
