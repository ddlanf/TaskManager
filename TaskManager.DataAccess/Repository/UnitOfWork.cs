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
            ProjectTasks = new TaskManagerDataAccess<ProjectTask>(_db);
            Employees = new TaskManagerDataAccess<Employee>(_db);
            Projects = new TaskManagerDataAccess<Project>(_db);
            EmployeeTasks = new TaskManagerDataAccess<EmployeeTask>(_db);
        }

        public ITaskManagerDataAccess<ProjectTask> ProjectTasks { get; }

        public ITaskManagerDataAccess<Employee> Employees { get; }

        public ITaskManagerDataAccess<Project> Projects { get; }

        public ITaskManagerDataAccess<EmployeeTask> EmployeeTasks { get; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
