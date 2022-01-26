using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BusinessLogic.Models;
using TaskManager.BusinessLogic.Models.ViewModels;

namespace TaskManager.BusinessLogic.Services
{
    public interface ITaskManagerService
    {
        IEnumerable<ProjectTaskModel> GetAllTasks();
        IEnumerable<ProjectTaskModel> GetAllTasks(int Id);

        IEnumerable<EmployeeModel> GetEmployees();
        IEnumerable<EmployeeModel> GetEmployees(int Id);
        IEnumerable<ProjectModel> GetProjects();
        void AddTask(TaskViewModel task);
    }
}
