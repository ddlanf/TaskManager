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
                _taskManagerService.AddTask(taskVM);
                TempData["TaskCreatedMessage"] = "Details added successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }

        public JsonResult OnGetEmployees(int projectId)
        {
            var employees = _taskManagerService.GetEmployees(projectId);
            return new JsonResult(employees);
        }

        public JsonResult OnGetProjects()
        {
            var projects = _taskManagerService.GetProjects();
            return new JsonResult(projects);
        }
    }
}
