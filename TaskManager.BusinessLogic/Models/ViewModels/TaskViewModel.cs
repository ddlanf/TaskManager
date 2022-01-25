using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BusinessLogic.Models.ViewModels
{
    public class TaskViewModel
    {
        public List<ProjectModel> Projects { get; set; } = new();

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public string StartDate { get; set; } = "";
        
        [Required]
        public string DueDate { get; set; } = "";

        [Required]
        public int AssignedProjectId { get; set; }
        public List<EmployeeModel> Employees { get; set; } = new();
        public List<int> AssignedEmployeeIds { get; set; } = new();

        public List<ProjectTaskModel> ProjectTasks { get; set; } = new();
    }
}
