using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TaskManager.BusinessLogic.Services;
using Moq;
using TaskManager.DataAccess.Repository.Interface;
using TaskManager.Pages;
using TaskManager.BusinessLogic.Models.ViewModels;
using TaskManager.Test;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.Test.Pages
{
    public class AssignModelTest
    {
        private readonly Mock<ITaskManagerService> _mockTaskManagerService = new Mock<ITaskManagerService>();
        private readonly AssignModel _pageModel;
        public AssignModelTest()
        {
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            _pageModel = new AssignModel(_mockTaskManagerService.Object) { 
                TempData = tempData,
            };
        }

        [Fact]
        public void OnPostReturnRedirectPageResultOnSuccess()
        {
            var taskVM = new TaskViewModel();
            var result = _pageModel.OnPost(taskVM);
            Assert.IsType<RedirectToPageResult>(result);
        }

        [Fact]
        public void OnPostReturnSetsTempDataOnSuccess()
        {
            var taskVM = new TaskViewModel();
            _pageModel.OnPost(taskVM);
            var tempData = _pageModel.TempData["TaskCreatedMessage"];
            Assert.Equal("Details added successfully", tempData);
        }

        [Fact]
        public void OnPostReturnPageResultOnModelStateError()
        {
            var taskVM = new TaskViewModel();
            _pageModel.ModelState.AddModelError("Error", "Sample error description");
            var result = _pageModel.OnPost(taskVM);
            _pageModel.ModelState.Clear();
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public void OnPostReturnsSetsErrorMessageOnServerError()
        {
            var taskVM = new TaskViewModel();
            _mockTaskManagerService.Setup(m => m.AddTask(taskVM)).Throws(new Exception());
            _pageModel.OnPost(taskVM);
            var message = taskVM.ServerErrorMessage;
            Assert.Equal("Failed to create a new task. Please try again later", message);
        }

        [Fact]
        public void OnPostReturnsPageResultOnServerError()
        {
            var taskVM = new TaskViewModel();
            _mockTaskManagerService.Setup(m => m.AddTask(taskVM)).Throws(new Exception());
            var result = _pageModel.OnPost(taskVM);
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public void OnGetEmployeesReturnsJsonResult()
        {
            var projectId = 1;
            var result = _pageModel.OnGetEmployees(projectId);
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void OnGetEmployeesReturnsJsonResultWithAListOfEmployeeObjectOnSuccess()
        {
            var projectId = 1;
            var result = _pageModel.OnGetEmployees(projectId);
            var expectedEmpployees = MockData.employeeModels;

            _mockTaskManagerService.Setup(m => m.GetEmployeesByProjectId(projectId))
                .Returns(expectedEmpployees);
            var resultJsonString = JsonConvert.SerializeObject(result.Value);
            var json = JsonConvert.DeserializeObject<List<EmployeeModel>>(resultJsonString);
            for (int i = 0; i < json!.Count; i++)
            {
                Assert.Equal(expectedEmpployees[i].EmploymentType, json[i].EmploymentType);
                Assert.Equal(expectedEmpployees[i].FirstName, json[i].FirstName);
                Assert.Equal(expectedEmpployees[i].LastName, json[i].LastName);
                Assert.Equal(expectedEmpployees[i].Mid, json[i].Mid);
            }
        }

        [Fact]
        public void OnGetEmployeesReturnsErrorMessageOnServerError()
        {
            var projectId = 1;
            _mockTaskManagerService.Setup(m => m.GetEmployeesByProjectId(projectId)).Throws(new Exception());
            var result = _pageModel.OnGetEmployees(projectId);
            var resultJsonString = JsonConvert.SerializeObject(result.Value);
            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultJsonString);
            Assert.Equal("There was an error retrieving employees", json!["error"]);
        }


        [Fact]
        public void OnGetEmployeesSetsErrorMessageOnServerError()
        {
            var projectId = 1;
            _mockTaskManagerService.Setup(m => m.GetEmployeesByProjectId(projectId)).Throws(new Exception());
            _pageModel.OnGetEmployees(projectId);
            var message = _pageModel.TaskVM.ServerErrorMessage;
            Assert.Equal("Failed to retrieve required info. Please try again later", message);
        }

        [Fact]
        public void OnGetProjectsReturnJsonResult()
        {
            var result = _pageModel.OnGetProjects();
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void OnGetProjectsReturnJsonResultWithWithAListOfProjectObjectOnSuccess()
        {
            var result = _pageModel.OnGetProjects();
            var expectedProjects = MockData.projectModels;
            var resultJsonString = JsonConvert.SerializeObject(result.Value);
            var json = JsonConvert.DeserializeObject<List<ProjectModel>>(resultJsonString);
            for (int i = 0; i < json!.Count; i++)
            {
                Assert.Equal(expectedProjects[i].Id, json[i].Id);
                Assert.Equal(expectedProjects[i].ProjectName, json[i].ProjectName);
            }
        }

        [Fact]
        public void OnGetProjectsReturnsErrorMessageOnServerError()
        {
            _mockTaskManagerService.Setup(m => m.GetProjects()).Throws(new Exception());
            var result = _pageModel.OnGetProjects();
            var resultJsonString = JsonConvert.SerializeObject(result.Value);
            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultJsonString);
            Assert.Equal("There was an error retrieving projects", json!["error"]);
        }

        [Fact]
        public void OnGetProjectsSetsErrorMessageOnServerError()
        {
            _mockTaskManagerService.Setup(m => m.GetProjects()).Throws(new Exception());
            _pageModel.OnGetProjects();
            var message = _pageModel.TaskVM.ServerErrorMessage;
            Assert.Equal("Failed to retrieve required info. Please try again later", message);
        }
    }
}
