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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagerContext _db;

        public UnitOfWork(TaskManagerContext db)
        {
            _db = db;
            ProjectTasks = new TaskDataAccess(_db);
            Employees = new EmployeeDataAccess(_db);
            Projects = new ProjectDataAccess(_db);
        }

        public ITaskDataAccess ProjectTasks { get; }
        public IEmployeeDataAccess Employees { get; }
        public IProjectDataAccess Projects { get; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
