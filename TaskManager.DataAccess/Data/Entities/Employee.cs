using System;
using System.Collections.Generic;

namespace TaskManager.DataAccess.Data.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            Tasks = new HashSet<ProjectTask>();
        }

        public int Mid { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? EmploymentType { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; } = null!;

        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }
}
