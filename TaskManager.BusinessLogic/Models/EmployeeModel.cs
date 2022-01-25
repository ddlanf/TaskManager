using System;
using System.Collections.Generic;

namespace TaskManager.BusinessLogic.Models
{
    public partial class EmployeeModel
    {
        public int Mid { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? EmploymentType { get; set; }
    }
}
