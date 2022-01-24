using System;
using System.Collections.Generic;

namespace TaskManager.BusinessLogic.Models
{
    public partial class Employee
    {
        public int Mid { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? EmploymentType { get; set; }
        public virtual ICollection<Task>? Tasks { get; set; }
    }
}
