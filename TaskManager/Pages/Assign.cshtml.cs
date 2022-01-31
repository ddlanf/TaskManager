using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.BusinessLogic.Models.ViewModels;
using TaskManager.BusinessLogic.Models;
using TaskManager.BusinessLogic.Services;

namespace TaskManager.Pages
{
    public class AssignModel : PageModel
    {
        private const string V = "Employees";

        public TaskViewModel TaskVM { get; set; } = new();
        private ITaskManagerService _taskManagerService { get; }
        public AssignModel(ITaskManagerService taskManagerService)
        {
            _taskManagerService = taskManagerService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(TaskViewModel taskVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _taskManagerService.AddTask(taskVM);
                    TempData["TaskCreatedMessage"] = "Details added successfully";
                    return RedirectToPage("Index");
                }
                catch (Exception ex)
                {
                    taskVM.ServerErrorMessage = "Failed to create a new task. Please try again later";
                    return Page();
                }
            }
            return Page();
        }

        public JsonResult OnGetEmployees(int projectId)
        {
            try 
            {
                var employees = _taskManagerService.GetEmployeesByProjectId(projectId);
                return new JsonResult(employees);
            }
            catch
            {
                TaskVM.ServerErrorMessage = "Failed to retrieve required info. Please try again later";
                return new JsonResult(new { error = "There was an error retrieving employees" });
            }
        }

        public JsonResult OnGetProjects()
        {
            try
            {
                var projects = _taskManagerService.GetProjects();
                return new JsonResult(projects);
            }
            catch
            {
                TaskVM.ServerErrorMessage = "Failed to retrieve required info. Please try again later";
                return new JsonResult(new { error = "There was an error retrieving projects" });
            }
        }
    }
}
