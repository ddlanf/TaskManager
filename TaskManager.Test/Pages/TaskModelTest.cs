using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BusinessLogic.Models;
using TaskManager.BusinessLogic.Models.ViewModels;
using TaskManager.BusinessLogic.Services;
using TaskManager.Pages;
using Xunit;

namespace TaskManager.Test.Pages
{
    public class TaskModelTest
    {
        private readonly Mock<ITaskManagerService> _mockTaskManagerService = new Mock<ITaskManagerService>();
        private readonly TaskModel _pageModel;
 
        public TaskModelTest()
        {
            _pageModel = new TaskModel(_mockTaskManagerService.Object);
        }

        [Fact]
        public void OnGetRetrievesProjectsAndAssignsToTaskVM()
        {

            var expectedProjects = MockData.projectModels;
            _mockTaskManagerService.Setup(m => m.GetProjects())
                        .Returns(expectedProjects);
            _pageModel.OnGet();

            var projects = _pageModel.TaskVM.Projects;
            for (int i = 0; i < projects!.Count; i++)
            {
                Assert.Equal(expectedProjects[i].Id, projects[i].Id);
                Assert.Equal(expectedProjects[i].ProjectName, projects[i].ProjectName);
            }
        }
        
        [Fact]
        public void OnGetRetrievesTasksAndAssignsToTaskVM()
        {
            var expectedTasks = MockData.projectTaskModels;
            _mockTaskManagerService.Setup(m => m.GetAllTasks())
                        .Returns(expectedTasks);
            _pageModel.OnGet();

            var tasks = _pageModel.TaskVM.ProjectTasks;
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
        public void OnGetSetsErrorMessageOnServerError()
        {
            var expectedProjects = MockData.projectModels;
            _mockTaskManagerService.Setup(m => m.GetProjects())
                        .Throws(new Exception());
            _mockTaskManagerService.Setup(m => m.GetAllTasks())
                      .Throws(new Exception());
            _pageModel.OnGet();

            Assert.Equal("Failed to retrieve required information", _pageModel.TaskVM.ServerErrorMessage);
        }


        [Fact]
        public void OnPostSetsProjectId()
        {
            TaskViewModel taskVM = new()
            {
                AssignedProjectId = 1
            };
            _pageModel.OnPost(taskVM);

            Assert.Equal(1, _pageModel.TaskVM.AssignedProjectId);
        }

        [Fact]
        public void OnPostSetsAllTasksIfAssignedProjectIdIsZero()
        {
            TaskViewModel taskVM = new()
            {
                AssignedProjectId = 0
            };
            var expectedTasks = MockData.projectTaskModels;
            _mockTaskManagerService.Setup(m => m.GetAllTasks())
                        .Returns(expectedTasks);

            _pageModel.OnPost(taskVM);

            var tasks = _pageModel.TaskVM.ProjectTasks;
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
        public void OnPostSetsTasksByIdIfAssignedProjectIdIsNotZero()
        {
            TaskViewModel taskVM = new()
            {
                AssignedProjectId = 1
            };
            var expectedTasks = MockData.projectTaskModels.Where(task=>task.Project!.Id == taskVM.AssignedProjectId).ToList();
            _mockTaskManagerService.Setup(m => m.GetAllTasks())
                        .Returns(expectedTasks);

            _pageModel.OnPost(taskVM);

            var tasks = _pageModel.TaskVM.ProjectTasks;
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
        public void OnPostSetsAllProjects ()
        {
            TaskViewModel taskVM = new()
            {
                AssignedProjectId = 0
            };
            var expectedProjects = MockData.projectModels;
            _mockTaskManagerService.Setup(m => m.GetProjects())
                        .Returns(expectedProjects);
            _pageModel.OnPost(taskVM);

            var projects = _pageModel.TaskVM.Projects;
            for (int i = 0; i < projects!.Count; i++)
            {
                Assert.Equal(expectedProjects[i].Id, projects[i].Id);
                Assert.Equal(expectedProjects[i].ProjectName, projects[i].ProjectName);
            }
        }

        [Fact]
        public void OnPostSetsErrorMessageOnServerError()
        {
            TaskViewModel taskVM = new()
            {
                AssignedProjectId = 0
            };
            var expectedProjects = MockData.projectModels;
            _mockTaskManagerService.Setup(m => m.GetProjects())
                        .Throws(new Exception());
            _mockTaskManagerService.Setup(m => m.GetAllTasks())
                       .Throws(new Exception());

            _pageModel.OnPost(taskVM);
            Assert.Equal("Failed to retrieve required information", _pageModel.TaskVM.ServerErrorMessage);
        }
    }
}
