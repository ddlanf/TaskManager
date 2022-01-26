using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models
{
    public partial class Task
    {
        public Task()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
