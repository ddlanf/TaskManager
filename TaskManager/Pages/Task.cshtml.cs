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
            TaskVM.ProjectTasks = (List<ProjectTaskModel>)_taskManagerService.GetAllTasks();
            TaskVM.Projects = (List<ProjectModel>)_taskManagerService.GetProjects();
        }

        public void OnPost(TaskViewModel taskVM)
        {
            TaskVM.AssignedProjectId = taskVM.AssignedProjectId;
            if (TaskVM.AssignedProjectId == 0)
            {
                TaskVM.ProjectTasks = (List<ProjectTaskModel>)_taskManagerService.GetAllTasks();
            }
            else
            {
                TaskVM.ProjectTasks = (List<ProjectTaskModel>)_taskManagerService.GetAllTasks(taskVM.AssignedProjectId);
            }
            TaskVM.Projects = (List<ProjectModel>)_taskManagerService.GetProjects();
        }
    }
}

