using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BusinessLogic.Models;
using TaskManager.BusinessLogic.Models.ViewModels;
using TaskManager.DataAccess.Data.Entities;
using TaskManager.DataAccess.Repository.Interface;

namespace TaskManager.BusinessLogic.Services
{
    public class TaskManagerService : ITaskManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddTask(TaskViewModel task)
        {
            var dbEmployees = (ICollection<Employee>) _unitOfWork.Employees.GetAll(emp=>task.AssignedEmployeeIds.Contains(emp.Mid));
            var dbTask = new ProjectTask()
            {
                Description = task.Description,
                StartDate = DateTimeOffset.Parse(task.StartDate),
                DueDate = DateTimeOffset.Parse(task.DueDate),
                ProjectId = task.AssignedProjectId,
                Employees = dbEmployees
            };
            _unitOfWork.ProjectTasks.Add(dbTask);
            _unitOfWork.Save();
        }

        public IEnumerable<ProjectTaskModel> GetAllTasks()
        {
            try
            {
                var dbTasks = _unitOfWork.ProjectTasks.GetAll(null,"Project,Employees").ToList();
                return ConvertDbTaskToTaskModel(dbTasks);
            }
            catch(Exception e) 
            {
                return new List<ProjectTaskModel>();
            }
           
        }

        public IEnumerable<ProjectTaskModel> GetAllTasks(int Id)
        {
            try
            {
                var dbTasks = _unitOfWork.ProjectTasks.GetAll(t=>t.ProjectId==Id, "Project,Employees").ToList();
                return ConvertDbTaskToTaskModel(dbTasks);
            }
            catch (Exception e)
            {
                return new List<ProjectTaskModel>();
            }
        }

        private IEnumerable<ProjectTaskModel> ConvertDbTaskToTaskModel(List<ProjectTask> dbTasks)
        {
            return dbTasks.ConvertAll(t => new ProjectTaskModel()
            {
                Id = t.Id,
                Description = t.Description,
                StartDate = t.StartDate,
                DueDate = t.DueDate,
                Project = new ProjectModel()
                {
                    Id = t.Project.Id,
                    ProjectName = t.Project.ProjectName
                },
                Employees = t.Employees.ToList().ConvertAll(e => new EmployeeModel()
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    EmploymentType = e.EmploymentType,
                    Mid = e.Mid,
                })
            });
        }

        public IEnumerable<EmployeeModel> GetEmployees()
        {
            var dbEmployees = (List<Employee>)_unitOfWork.Employees.GetAll();
            return ConvertDbEmployeeToEmployeeModel(dbEmployees);
        }

        public IEnumerable<EmployeeModel> GetEmployees(int id)
        {
            var dbEmployees = (List<Employee>)_unitOfWork.Employees.GetAll(emp=>emp.ProjectId == id);
            return ConvertDbEmployeeToEmployeeModel(dbEmployees);
        }

        private IEnumerable<EmployeeModel> ConvertDbEmployeeToEmployeeModel(List<Employee> dbEmployees)
        {
            return dbEmployees.ConvertAll(e => new EmployeeModel()
            {
                Mid = e.Mid,
                EmploymentType = e.EmploymentType,
                FirstName = e.FirstName,
                LastName = e.LastName,
            });
        }

        public IEnumerable<ProjectModel> GetProjects()
        {
            var dbProjects = _unitOfWork.Projects.GetAll() as List<Project>;
            return dbProjects!.ConvertAll(p => new ProjectModel()
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Tasks = new List<ProjectTaskModel>()
            });
        }
    }
}
