using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BusinessLogic.Models;
using TaskManager.DataAccess.Data.Entities;

namespace TaskManager.Test
{
    static public class MockData 
    {
       
        static public List<ProjectModel> GetProjectModels() => new() {
               new ProjectModel()
               {
                   ProjectName = "Azure Capacity",
                   Id = 1
               },
               new ProjectModel()
               {
                   ProjectName = "Azure Billing",
                   Id = 2
               }
            };

        static public List<EmployeeModel> GetEmployeeModels() => new List<EmployeeModel>() {
                    new EmployeeModel()
                    {
                        EmploymentType = "Contract",
                        FirstName = "Joe",
                        LastName = "Rogan",
                        Mid = 1,
                    },
                     new EmployeeModel()
                    {
                        EmploymentType = "Full Time",
                        FirstName = "Sally",
                        LastName = "May",
                        Mid = 2,
                    },
                    new EmployeeModel()
                    {
                        EmploymentType = "Full Time",
                        FirstName = "Paul",
                        LastName = "Lee",
                        Mid = 3,
                    }
                 };
        static public List<ProjectTaskModel> GetProjectTaskModels() => new List<ProjectTaskModel>() {
                   new ProjectTaskModel()
                   {
                       Id = 1, 
                       StartDate = DateTimeOffset.Parse("1-01-2020"), 
                       DueDate = DateTimeOffset.Parse("1-01-2022"), 
                       Description = "This is a task",
                       Employees = GetEmployeeModels().Take(2).ToList(),
                       Project = GetProjectModels()[0]
                   },
                    new ProjectTaskModel()
                   {
                       Id = 2,
                       StartDate = DateTimeOffset.Parse("1-01-2020"),
                       DueDate = DateTimeOffset.Parse("1-01-2022"),
                       Description = "This is another task",
                       Employees = GetEmployeeModels().Take(2).ToList(),
                       Project = GetProjectModels()[0]
                   }
                 };


        static public List<Project> GetProjects() => new List<Project>() {
               new Project()
               {
                   ProjectName = "Azure Capacity",
                   Id = 1,
               },
               new Project()
               {
                   ProjectName = "Azure Billing",
                   Id = 2,
               }
            };

        static public List<Employee> GetEmployees() => new List<Employee>() {
                new Employee()
                {
                    EmploymentType = "Contract",
                    FirstName = "Joe",
                    LastName = "Rogan",
                    Mid = 1,
                    ProjectId = 1,
                    Project = GetProjects()[0]
                },
                new Employee()
                {
                    EmploymentType = "Full Time",
                    FirstName = "Sally",
                    LastName = "May",
                    Mid = 2,
                    ProjectId = 1,
                    Project = GetProjects()[0]
                },
                new Employee()
                {
                    EmploymentType = "Full Time",
                    FirstName = "Paul",
                    LastName = "Lee",
                    Mid = 3,
                    ProjectId = 2,
                    Project = GetProjects()[1]
                }
            };
        static public List<ProjectTask> GetProjectTasks() => new List<ProjectTask>() {
                   new ProjectTask()
                   {
                       Id = 1,
                      StartDate = DateTimeOffset.Parse("1-01-2020"),
                       DueDate = DateTimeOffset.Parse("1-01-2022"),
                       Description = "This is a task",
                       Employees = GetEmployees().Where(emp=>emp.ProjectId==1).ToList(),
                       Project = GetProjects()[0]
                   },
                    new ProjectTask()
                   {
                       Id = 2,
                       StartDate = DateTimeOffset.Parse("1-01-2020"),
                       DueDate = DateTimeOffset.Parse("1-01-2022"),
                       Description = "This is another task",
                       Employees = GetEmployees().Where(emp=>emp.ProjectId==1).ToList(),
                       Project = GetProjects()[0]
                   }
                 };
    }

}
