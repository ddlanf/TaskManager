using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Data.Entities;
using TaskManager.DataAccess.Repository;
using TaskManager.DataAccess.Repository.Interface;
using Xunit;

namespace TaskManager.Test.DataAccess
{
    public class DataAccessTest
    {
        private readonly Mock<TaskManagerContext> _mockdbContext = new();
        private readonly IUnitOfWork _unitOfWork;
        public DataAccessTest()
        {
            IList<Employee> employees = MockData.GetEmployees();
            IList<Project> projects = MockData.GetProjects();
            IList<ProjectTask> tasks = MockData.GetProjectTasks();
            _mockdbContext.Setup(x => x.Employees).ReturnsDbSet(employees);
            _mockdbContext.Setup(x => x.Projects).ReturnsDbSet(projects);
            _mockdbContext.Setup(x => x.Tasks).ReturnsDbSet(tasks);
            _mockdbContext.Setup(x => x.Set<Employee>()).ReturnsDbSet(employees);
            _mockdbContext.Setup(x => x.Set<Project>()).ReturnsDbSet(projects);
            _mockdbContext.Setup(x => x.Set<ProjectTask>()).ReturnsDbSet(tasks);

            _unitOfWork = new UnitOfWork(_mockdbContext.Object);
        }

        //Employee DataAccess 
        [Fact]
        public void EmployeeDataAccessGetsAllEmployees()
        {
            var expectedEmployees = MockData.GetEmployees();
            var employees = _unitOfWork.Employees.GetAll(null, null).ToList();
            for (int i = 0; i < employees!.Count; i++)
            {
                Assert.Equal(expectedEmployees[i].Mid, employees[i].Mid);
                Assert.Equal(expectedEmployees[i].FirstName, employees[i].FirstName);
                Assert.Equal(expectedEmployees[i].LastName, employees[i].LastName);
                Assert.Equal(expectedEmployees[i].EmploymentType, employees[i].EmploymentType);
            }
        }

        [Fact]
        public void EmployeeDataAccessGetsAllEmployeesByFilterCondition()
        {
            var projectId = 1;
            var expectedEmployees = MockData.GetEmployees().Where(emp => emp.ProjectId == projectId).ToList();
            var employees = _unitOfWork.Employees.GetAll(emp=>emp.ProjectId == projectId, null).ToList();
            for (int i = 0; i < employees!.Count; i++)
            {
                Assert.Equal(expectedEmployees[i].Mid, employees[i].Mid);
                Assert.Equal(expectedEmployees[i].FirstName, employees[i].FirstName);
                Assert.Equal(expectedEmployees[i].LastName, employees[i].LastName);
                Assert.Equal(expectedEmployees[i].EmploymentType, employees[i].EmploymentType);
            }
        }

        [Fact]
        public void EmployeeDataAccessGetsAllEmployeesWithInclude()
        {
            var expectedEmployees = MockData.GetEmployees().ToList();
            var employees = _unitOfWork.Employees.GetAll(null, "Project").ToList();
            for (int i = 0; i < employees!.Count; i++)
            {
                Assert.Equal(expectedEmployees[i].Mid, employees[i].Mid);
                Assert.Equal(expectedEmployees[i].FirstName, employees[i].FirstName);
                Assert.Equal(expectedEmployees[i].LastName, employees[i].LastName);
                Assert.Equal(expectedEmployees[i].EmploymentType, employees[i].EmploymentType);
                Assert.Equal(expectedEmployees[i].Project.Id, employees[i].Project.Id);
                Assert.Equal(expectedEmployees[i].Project.ProjectName, employees[i].Project.ProjectName);
            }
        }

        [Fact]
        public void EmployeeDataAccessAddMethodRunsAsExpected()
        {
            var newEmployee = new Employee()
            {
                Mid = 4, 
                FirstName = "Hector", 
                LastName = "Jones", 
                EmploymentType = "Full Time", 
                ProjectId = 1, 
                Project = MockData.GetProjects().FirstOrDefault(p=>p.Id == 1) ?? new()
            };

            _unitOfWork.Employees.Add(newEmployee);

            Assert.True(true);
        }

        //Projects
        [Fact]
        public void ProjectDataAccessGetsAllEmployees()
        {
            var expectedProjects = MockData.GetProjects();
            var projects = _unitOfWork.Projects.GetAll(null, null).ToList();
            for (int i = 0; i < projects!.Count; i++)
            {
                Assert.Equal(expectedProjects[i].Id, projects[i].Id);
                Assert.Equal(expectedProjects[i].ProjectName, projects[i].ProjectName);
            }
        }

        [Fact]
        public void ProjectDataAccessGetsAllEmployeesByFilterCondition()
        {
            var projectId = 1;
            var expectedProjects = MockData.GetProjects().Where(p => p.Id == projectId).ToList();
            var projects = _unitOfWork.Projects.GetAll(p => p.Id == projectId, null).ToList();
            for (int i = 0; i < projects!.Count; i++)
            {
                Assert.Equal(expectedProjects[i].Id, projects[i].Id);
                Assert.Equal(expectedProjects[i].ProjectName, projects[i].ProjectName);
            }
        }

        [Fact]
        public void ProjectDataAccessGetsAllEmployeesWithInclude()
        {
            var expectedProjects = MockData.GetProjects();
            var projects = _unitOfWork.Projects.GetAll(null, "Employees;Tasks").ToList();
            for (int i = 0; i < projects!.Count; i++)
            {
                Assert.Equal(expectedProjects[i].Id, projects[i].Id);
                Assert.Equal(expectedProjects[i].ProjectName, projects[i].ProjectName);
            }
        }

        [Fact]
        public void ProjectDataAccessAddMethodRunsAsExpected()
        {
            var newProject = new Project()
            {
               Id = 3, 
               ProjectName = "Hello Project", 
               Employees = new List<Employee>(),
               Tasks = new List<ProjectTask>()
            };

            _unitOfWork.Projects.Add(newProject);

            Assert.True(true);
        }


        //Task
        [Fact]
        public void TasksDataAccessGetsAllEmployees()
        {
            var expectedTasks = MockData.GetProjectTasks();
            var tasks = _unitOfWork.ProjectTasks.GetAll(null, null).ToList();
            for (int i = 0; i < tasks!.Count; i++)
            {
                Assert.Equal(expectedTasks[i].Id, tasks[i].Id);
                Assert.Equal(expectedTasks[i].StartDate, tasks[i].StartDate);
                Assert.Equal(expectedTasks[i].DueDate, tasks[i].DueDate);
                Assert.Equal(expectedTasks[i].Description, tasks[i].Description);
                Assert.Equal(expectedTasks[i].Project!.Id, tasks[i].Project!.Id);
                Assert.Equal(expectedTasks[i].Project!.ProjectName, tasks[i].Project!.ProjectName);
                for (int j = 0; j < tasks[i].Employees!.Count; j++)
                {
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).Mid, tasks[i].Employees!.ElementAt(j).Mid);
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).FirstName, tasks[i].Employees!.ElementAt(j).FirstName);
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).LastName, tasks[i].Employees!.ElementAt(j).LastName);
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).EmploymentType, tasks[i].Employees!.ElementAt(j).EmploymentType);
                }
            }
        }

        [Fact]
        public void TasksDataAccessGetsAllEmployeesByFilterCondition()
        {
            var projectId = 1;
            var expectedTasks = MockData.GetProjectTasks().Where(p => p.Id == projectId).ToList();
            var tasks = _unitOfWork.ProjectTasks.GetAll(emp => emp.ProjectId == projectId, null).ToList();
            for (int i = 0; i < tasks!.Count; i++)
            {
                Assert.Equal(expectedTasks[i].Id, tasks[i].Id);
                Assert.Equal(expectedTasks[i].StartDate, tasks[i].StartDate);
                Assert.Equal(expectedTasks[i].DueDate, tasks[i].DueDate);
                Assert.Equal(expectedTasks[i].Description, tasks[i].Description);
                Assert.Equal(expectedTasks[i].Project!.Id, tasks[i].Project!.Id);
                Assert.Equal(expectedTasks[i].Project!.ProjectName, tasks[i].Project!.ProjectName);
                for (int j = 0; j < tasks[i].Employees!.Count; j++)
                {
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).Mid, tasks[i].Employees!.ElementAt(j).Mid);
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).FirstName, tasks[i].Employees!.ElementAt(j).FirstName);
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).LastName, tasks[i].Employees!.ElementAt(j).LastName);
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).EmploymentType, tasks[i].Employees!.ElementAt(j).EmploymentType);
                }
            }
        }

        [Fact]
        public void TasksDataAccessGetsAllEmployeesWithInclude()
        {
            var expectedTasks = MockData.GetProjectTasks();
            var tasks = _unitOfWork.ProjectTasks.GetAll(null, "Employees,Project").ToList();
            for (int i = 0; i < tasks!.Count; i++)
            {
                Assert.Equal(expectedTasks[i].Id, tasks[i].Id);
                Assert.Equal(expectedTasks[i].StartDate, tasks[i].StartDate);
                Assert.Equal(expectedTasks[i].DueDate, tasks[i].DueDate);
                Assert.Equal(expectedTasks[i].Description, tasks[i].Description);
                Assert.Equal(expectedTasks[i].Project!.Id, tasks[i].Project!.Id);
                Assert.Equal(expectedTasks[i].Project!.ProjectName, tasks[i].Project!.ProjectName);
                for (int j = 0; j < tasks[i].Employees!.Count; j++)
                {
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).Mid, tasks[i].Employees!.ElementAt(j).Mid);
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).FirstName, tasks[i].Employees!.ElementAt(j).FirstName);
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).LastName, tasks[i].Employees!.ElementAt(j).LastName);
                    Assert.Equal(expectedTasks[i].Employees!.ElementAt(j).EmploymentType, tasks[i].Employees!.ElementAt(j).EmploymentType);
                }
            }
        }

        [Fact]
        public void TasksDataAccessAddMethodRunsAsExpected()
        {
            var newTask= new ProjectTask()
            {
                Id = 3,
                Description = "Hello Project",
                Employees = new List<Employee>(),
                ProjectId = 1, 
                Project = new Project(),
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
            };

            _unitOfWork.ProjectTasks.Add(newTask);
            Assert.True(true);
        }

    }
}