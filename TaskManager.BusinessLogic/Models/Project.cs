using System;
using System.Collections.Generic;

namespace TaskManager.BusinessLogic.Models
{
    public partial class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = null!;

        public ICollection<Task>? Tasks { get; set; }
    }
}
