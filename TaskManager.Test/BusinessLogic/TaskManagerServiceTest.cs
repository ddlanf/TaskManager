using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BusinessLogic.Models;
using TaskManager.BusinessLogic.Models.ViewModels;
using TaskManager.BusinessLogic.Services;
using TaskManager.DataAccess.Data.Entities;
using TaskManager.DataAccess.Repository.Interface;
using Xunit;

namespace TaskManager.Test.BusinessLogic
{
    public class TaskManagerServiceTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();
        private readonly ITaskManagerService _taskManagerService;
        public TaskManagerServiceTest()
        {
            _taskManagerService = new TaskManagerService(_mockUnitOfWork.Object);
        }

        [Fact]
        public void GetAllTasksReturnAListOfTaskModels()
        {
            var expectedTasks = MockData.GetProjectTaskModels();
            _mockUnitOfWork.Setup(db => db.ProjectTasks.GetAll(null, "Project,Employees")).Returns(MockData.GetProjectTasks());

            var tasks = _taskManagerService.GetAllTasks().ToList();
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
        public void GetAllTasksThrowsExceptionOnDatabaseError()
        {
            var expectedTasks = MockData.GetProjectTaskModels();
            _mockUnitOfWork.Setup(db => db.ProjectTasks.GetAll(null, "Project,Employees")).Throws(new Exception());

            Assert.Throws<Exception>(_taskManagerService.GetAllTasks);
        }

        [Fact]
        public void GetAllTasksWithIdReturnAListOfTaskModelsById()
        {
            var projectId = 1;
            var expectedTasks = MockData.GetProjectTaskModels().Where(t => t.Project!.Id == projectId).ToList();
            _mockUnitOfWork.Setup(db => db.ProjectTasks.GetAll(t => t.ProjectId == projectId, "Project,Employees")).Returns(MockData.GetProjectTasks().Where(t => t.Project!.Id == projectId).ToList());

            var tasks = _taskManagerService.GetAllTasks().ToList();
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
        public void GetAllTasksWithIdThrowsExceptionOnDatabaseError()
        {
            var projectId = 1;
            _mockUnitOfWork.Setup(db => db.ProjectTasks.GetAll(t => t.ProjectId == projectId, "Project,Employees")).Throws(new Exception());
            Assert.Throws<Exception>(() => _taskManagerService.GetAllTasks(projectId));
        }

        [Fact]
        public void AddTaskSuccessfullyAddsNewTask()
        {
            var exampleTaskVM = new TaskViewModel()
            {
                Description = "Hello world",
                StartDate = "01-20-2020",
                DueDate = "01-20-2022",
                AssignedProjectId = 1,
                AssignedEmployeeIds = { 1, 2, }
            };

            var expectedTaskModelTobeReturned = new ProjectTaskModel()
            {
                Id = 3,
                StartDate = DateTimeOffset.Parse("1-20-2020"),
                DueDate = DateTimeOffset.Parse("1-20-2022"),
                Description = "Hello world",
                Employees = MockData.GetEmployeeModels().Take(2).ToList(),
                Project = MockData.GetProjectModels()[0]
            };

            var expectedTaskTobeAdded = new ProjectTask()
            {
                Id = 3,
                Description = exampleTaskVM.Description,
                StartDate = DateTimeOffset.Parse(exampleTaskVM.StartDate),
                DueDate = DateTimeOffset.Parse(exampleTaskVM.DueDate),
                ProjectId = exampleTaskVM.AssignedProjectId,
                Project = MockData.GetProjects().FirstOrDefault(p => p.Id == exampleTaskVM.AssignedProjectId) ?? new(),
                Employees = MockData.GetEmployees().Where(emp => exampleTaskVM.AssignedEmployeeIds.Contains(emp.Mid)).ToList()
            };

            var taskList = MockData.GetProjectTasks();

            _mockUnitOfWork
                .Setup(t => t.Employees.GetAll(emp => exampleTaskVM.AssignedEmployeeIds.Contains(emp.Mid), null))
                .Returns(MockData.GetEmployees().Where(emp => exampleTaskVM.AssignedEmployeeIds.Contains(emp.Mid)).ToList());

            _mockUnitOfWork
                .Setup(db => db.ProjectTasks.GetAll(null, "Project,Employees"))
                .Returns(() =>
                {
                    taskList.Add(expectedTaskTobeAdded);
                    return taskList;
                });

            //Add Task
            _taskManagerService.AddTask(exampleTaskVM);

            var taskListAfterAddTask = _taskManagerService.GetAllTasks().ToList();

            var newTaskId = 3;
            var newTask = taskListAfterAddTask.Where(t => t.Id == newTaskId).FirstOrDefault() ?? new ProjectTaskModel();

            Assert.Equal(expectedTaskModelTobeReturned.Id, newTask.Id);
            Assert.Equal(expectedTaskModelTobeReturned.StartDate, newTask.StartDate);
            Assert.Equal(expectedTaskModelTobeReturned.DueDate, newTask.DueDate);
            Assert.Equal(expectedTaskModelTobeReturned.Description, newTask.Description);
            Assert.Equal(expectedTaskModelTobeReturned.Project!.Id, newTask.Project!.Id);
            Assert.Equal(expectedTaskModelTobeReturned.Project!.ProjectName, newTask.Project!.ProjectName);
            for (int j = 0; j < newTask.Employees!.Count; j++)
            {
                Assert.Equal(expectedTaskModelTobeReturned.Employees!.ElementAt(j).Mid, newTask.Employees!.ElementAt(j).Mid);
                Assert.Equal(expectedTaskModelTobeReturned.Employees!.ElementAt(j).FirstName, newTask.Employees!.ElementAt(j).FirstName);
                Assert.Equal(expectedTaskModelTobeReturned.Employees!.ElementAt(j).LastName, newTask.Employees!.ElementAt(j).LastName);
                Assert.Equal(expectedTaskModelTobeReturned.Employees!.ElementAt(j).EmploymentType, newTask.Employees!.ElementAt(j).EmploymentType);
            }
        }

        [Fact]
        public void AddTaskThrowsExceptionOnDatabaseError()
        {
            var exampleTaskVM = new TaskViewModel()
            {
                Description = "Hello world",
                StartDate = "01-20-2020",
                DueDate = "01-20-2022",
                AssignedProjectId = 1,
                AssignedEmployeeIds = { 1, 2, }
            };

            var expectedTaskTobeAdded = new ProjectTask()
            {
                Id = 0,
                Description = exampleTaskVM.Description,
                StartDate = DateTimeOffset.Parse(exampleTaskVM.StartDate),
                DueDate = DateTimeOffset.Parse(exampleTaskVM.DueDate),
                ProjectId = exampleTaskVM.AssignedProjectId,
                Project = MockData.GetProjects().FirstOrDefault(p => p.Id == exampleTaskVM.AssignedProjectId) ?? new(),
                Employees = MockData.GetEmployees().Where(emp => exampleTaskVM.AssignedEmployeeIds.Contains(emp.Mid)).ToList()
            };

            _mockUnitOfWork
                .Setup(t => t.Employees.GetAll(emp => exampleTaskVM.AssignedEmployeeIds.Contains(emp.Mid), null))
                .Throws(new Exception());

            _mockUnitOfWork
                 .Setup(t => t.ProjectTasks.Add(expectedTaskTobeAdded))
                 .Throws(new Exception());

            Assert.Throws<Exception>(()=>_taskManagerService.AddTask(exampleTaskVM));
        }

        [Fact]
        public void GetEmployeeByProjectIdReturnsEmployeesWithCorrectProjectID()
        {
            var projectId = 1;
            var expectedTasks = MockData.GetProjectTaskModels().Where(t => t.Project!.Id == projectId).ToList();
            var expectedEmployeesFromDb = MockData.GetEmployees().Where(emp => emp.ProjectId == projectId).ToList();
            var expectedEmployeeModels = MockData.GetEmployeeModels().Where(emp => expectedEmployeesFromDb.ConvertAll(dbemp=>dbemp.Mid).Contains(emp.Mid)).ToList();

            _mockUnitOfWork
                .Setup(db => db.Employees.GetEmployeesByProjectIdWithStoredProcedure(projectId))
                .Returns(expectedEmployeesFromDb);

            var employees = _taskManagerService.GetEmployeesByProjectId(projectId).ToList();
            for (int i = 0; i < employees!.Count; i++)
            {
                Assert.Equal(expectedEmployeeModels[i].Mid, employees[i].Mid);
                Assert.Equal(expectedEmployeeModels[i].FirstName, employees[i].FirstName);
                Assert.Equal(expectedEmployeeModels[i].LastName, employees[i].LastName);
                Assert.Equal(expectedEmployeeModels[i].EmploymentType, employees[i].EmploymentType);
            }
        }

        [Fact]
        public void GetEmployeeByProjectIdThrowsExceptionOnDatbaseError()
        {
            var projectId = 1;
            _mockUnitOfWork
                .Setup(db => db.Employees.GetEmployeesByProjectIdWithStoredProcedure(projectId))
                .Throws(new Exception());

            Assert.Throws<Exception>(()=>_taskManagerService.GetEmployeesByProjectId(projectId));
        }

        [Fact]
        public void GetProjectsReturnsProjectsModels()
        {
            var expectedProjectsFromDb = MockData.GetProjects().ToList();
            _mockUnitOfWork
                .Setup(db => db.Projects.GetProjectsWithStoredProcedure())
                .Returns(expectedProjectsFromDb);

            var tasks = _taskManagerService.GetProjects().ToList();

            for (int i = 0; i < tasks!.Count; i++)
            {
                Assert.Equal(expectedProjectsFromDb[i].Id, tasks[i].Id);
                Assert.Equal(expectedProjectsFromDb[i].ProjectName, tasks[i].ProjectName);
            }
        }

        [Fact]
        public void GetProjectsThrowsExceptionOnDatbaseError()
        {
            var expectedProjectsFromDb = MockData.GetProjects().ToList();
            _mockUnitOfWork
                .Setup(db => db.Projects.GetProjectsWithStoredProcedure())
                  .Throws(new Exception());

            Assert.Throws<Exception>(() => _taskManagerService.GetProjects());
        }
    }

}
