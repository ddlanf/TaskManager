using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.BusinessLogic.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTimeOffset StartDate { get; set; } 
        public DateTimeOffset DueDate { get; set; }
        public Employee? Employee { get; set; }
        public Project? Project { get; set; }
    }
}
