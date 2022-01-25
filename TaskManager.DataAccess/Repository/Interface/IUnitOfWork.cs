using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Data.Entities;

namespace TaskManager.DataAccess.Repository.Interface
{
    public interface IUnitOfWork
    {
        ITaskManagerDataAccess<ProjectTask> ProjectTasks { get; }
        ITaskManagerDataAccess<Employee> Employees { get; }
        ITaskManagerDataAccess<Project> Projects { get; }
        ITaskManagerDataAccess<EmployeeTask> EmployeeTasks { get; }
        void Save();
    }
}
