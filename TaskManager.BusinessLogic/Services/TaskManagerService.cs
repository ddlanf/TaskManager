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
            var dbTask = new ProjectTask()
            {
                Description = task.Description,
                StartDate = DateTimeOffset.Parse(task.StartDate),
                DueDate = DateTimeOffset.Parse(task.DueDate),
                ProjectId = task.AssignedProjectId,
            };

            _unitOfWork.ProjectTasks.Add(dbTask);
            
            var employeeIds = task.AssignedEmployeeIds;
            employeeIds.ForEach(id => _unitOfWork.EmployeeTasks.Add(
                new EmployeeTask()
                {
                    EmployeeId = id,
                    TaskId = dbTask.Id,
                })
            );
            _unitOfWork.Save();
        }

        public IEnumerable<ProjectTaskModel> GetAllTasks(int? projectId=0)
        {
            try
            {

                var dbTasks = _unitOfWork.ProjectTasks.GetAll
                   (projectId != 0 ? p => p.ProjectId == projectId : null,
                   "Project"
                ).ToList();
                
                var dbTaskIds = dbTasks.ConvertAll(t=>t.Id);
                var dbEmployeeTasksIds = _unitOfWork.EmployeeTasks.GetAll(et => dbTaskIds.Contains(et.TaskId ?? 0), includeProperties : "Employee").ToList();

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
                    Employees = dbEmployeeTasksIds.Where(et => et.TaskId == t.Id).ToList().ConvertAll(et => new EmployeeModel() { 
                        FirstName = et.Employee.FirstName, 
                        LastName = et.Employee.LastName,
                        EmploymentType = et.Employee.EmploymentType,
                        Mid = et.Employee.Mid,
                    })
                });
            }
            catch(Exception e) 
            {
                return new List<ProjectTaskModel>();
            }
           
        }

        public IEnumerable<EmployeeModel> GetEmployees()
        {
            var dbEmployees = _unitOfWork.Employees.GetAll() as List<Employee>;
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
            return dbProjects.ConvertAll(p => new ProjectModel()
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Tasks = new List<ProjectTaskModel>()
            });
        }
    }
}
