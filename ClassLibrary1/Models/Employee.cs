using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Tasks = new HashSet<Task>();
        }

        public int Mid { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? EmploymentType { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
