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
        public List<Project> Projects { get; set; } = new();

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public string StartDate { get; set; } = "";
        
        [Required]
        public string DueDate { get; set; } = "";

        [Required]
        public int AssignedProjectId { get; set; }
        public List<Employee> Employees { get; set; } = new();
        public List<int> AssignedEmployeeIds { get; set; } = new();
    }
}
