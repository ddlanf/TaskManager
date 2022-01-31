using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.BusinessLogic.Models.ViewModels;
using TaskManager.BusinessLogic.Models;
using TaskManager.BusinessLogic.Services;

namespace TaskManager.Pages
{
    public class TaskModel : PageModel
    {
        public TaskViewModel TaskVM { get; set; } = new();
        private ITaskManagerService _taskManagerService { get; }

        public TaskModel(ITaskManagerService taskManagerService)
        {
            _taskManagerService = taskManagerService;
        }

        public void OnGet()
        {
            try
            {
                TaskVM.ProjectTasks = _taskManagerService.GetAllTasks().ToList();
                TaskVM.Projects = _taskManagerService.GetProjects().ToList();
            }
            catch (Exception ex)
            {
                TaskVM.ServerErrorMessage = "Failed to retrieve required information";
            }
        }

        public void OnPost(TaskViewModel taskVM)
        {
            TaskVM.AssignedProjectId = taskVM.AssignedProjectId;
            try
            {
                if (TaskVM.AssignedProjectId == 0)
                {
                    TaskVM.ProjectTasks = _taskManagerService.GetAllTasks().ToList();
                }
                else
                {
                    TaskVM.ProjectTasks = _taskManagerService.GetAllTasks(taskVM.AssignedProjectId).ToList();
                }
                TaskVM.Projects = _taskManagerService.GetProjects().ToList();
            }
            catch (Exception ex)
            {
                TaskVM.ServerErrorMessage = "Failed to retrieve required information";
            }
        }
    }
}

