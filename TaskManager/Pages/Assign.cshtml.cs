using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.BusinessLogic.Models.ViewModels;
using TaskManager.BusinessLogic.Models;
using TaskManager.BusinessLogic.Services;

namespace TaskManager.Pages
{
    public class AssignModel : PageModel
    {
        public TaskViewModel TaskVM { get; set; } = new();
        private ITaskManagerService _taskManagerService { get; }
        public AssignModel(ITaskManagerService taskManagerService)
        {
            _taskManagerService = taskManagerService;
        }

        public void OnGet()
        {
            TaskVM.Projects = (List<ProjectModel>)_taskManagerService.GetProjects();
            TaskVM.Employees = (List<EmployeeModel>)_taskManagerService.GetEmployees();
        }

        public IActionResult OnPost(TaskViewModel taskVM)
        {
            _taskManagerService.AddTask(taskVM);
            TempData["TaskCreated"] = true;
            return RedirectToPage("Index");
        }
    }
}
