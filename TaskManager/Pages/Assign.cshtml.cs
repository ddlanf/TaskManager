using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.BusinessLogic.Models.ViewModels;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.Pages
{
    public class AssignModel : PageModel
    {
        public TaskViewModel TaskVM { get; set; } = new();

        public void OnGet()
        {
            TaskVM.Projects = new List<Project>() {
                new Project()
                {
                    Id = 1,
                    ProjectName = "Microsoft Azure Billing"
                },
                 new Project()
                {
                    Id = 2,
                    ProjectName = "Amazon Machine Learning Project"
                },

            };
            TaskVM.Employees = new List<Employee>() { 
                new Employee()
                {
                    Mid = 1234,
                    FirstName = "John", 
                    LastName = "Smith",
                    EmploymentType = "Contract"
                },
                new Employee()
                {
                    Mid = 2433,
                    FirstName = "Julia",
                    LastName = "Baker",
                    EmploymentType = "Full Time"
                },
                new Employee()
                {
                    Mid = 3343,
                    FirstName = "Holly",
                    LastName = "Fuller",
                    EmploymentType = "Full Time"
                },
                new Employee()
                {
                    Mid = 5444,
                    FirstName = "Joe",
                    LastName = "Trader",
                    EmploymentType = "Full Time"
                },
                new Employee()
                {
                    Mid = 5466,
                    FirstName = "Frank",
                    LastName = "Lod",
                    EmploymentType = "Full Time"
                },
                new Employee()
                {
                    Mid = 5,
                    FirstName = "Jackson",
                    LastName = "Paul",
                    EmploymentType = "Full Time"
                }
            };
            
        }

        public IActionResult OnPost(TaskViewModel taskVM)
        {
            TaskVM.Projects = new List<Project>() {
                new Project()
                {
                    Id = 1,
                    ProjectName = "Microsoft Azure Billing"
                },
                 new Project()
                {
                    Id = 2,
                    ProjectName = "Amazon Machine Learning Project"
                },

            };
            TaskVM.Employees = new List<Employee>() {
                new Employee()
                {
                    Mid = 1234,
                    FirstName = "John",
                    LastName = "Smith",
                    EmploymentType = "Contract"
                },
                new Employee()
                {
                    Mid = 2433,
                    FirstName = "Julia",
                    LastName = "Baker",
                    EmploymentType = "Full Time"
                },
                new Employee()
                {
                    Mid = 3343,
                    FirstName = "Holly",
                    LastName = "Fuller",
                    EmploymentType = "Full Time"
                },
                new Employee()
                {
                    Mid = 5444,
                    FirstName = "Joe",
                    LastName = "Trader",
                    EmploymentType = "Full Time"
                },
                new Employee()
                {
                    Mid = 5466,
                    FirstName = "Frank",
                    LastName = "Lod",
                    EmploymentType = "Full Time"
                },
                new Employee()
                {
                    Mid = 5,
                    FirstName = "Jackson",
                    LastName = "Paul",
                    EmploymentType = "Full Time"
                }
            };

            TempData["TaskCreated"] = true;
            return RedirectToPage("Index");
        }
    }
}
