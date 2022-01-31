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
       
        static public List<ProjectModel> projectModels = new List<ProjectModel>() {
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

        static public List<EmployeeModel> employeeModels = new List<EmployeeModel>() {
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
                        Mid = 3,
                    }
                 };
        static public List<ProjectTaskModel> projectTaskModels = new List<ProjectTaskModel>() {
                   new ProjectTaskModel()
                   {
                       Id = 1, 
                       StartDate = DateTimeOffset.Now, 
                       DueDate = DateTimeOffset.Now.AddDays(10), 
                       Description = "This is a task", 
                       Employees = employeeModels, 
                       Project = projectModels[0]
                   },
                    new ProjectTaskModel()
                   {
                       Id = 2,
                       StartDate = DateTimeOffset.Now,
                       DueDate = DateTimeOffset.Now.AddDays(15),
                       Description = "This is another task",
                       Employees = employeeModels,
                       Project = projectModels[0]
                   }
                 };

    }

}
