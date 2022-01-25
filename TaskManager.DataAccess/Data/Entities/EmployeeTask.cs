using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.DataAccess.Data.Entities
{
    public partial class EmployeeTask
    {
        public int? EmployeeId { get; set; }
        public int? TaskId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual ProjectTask? Task { get; set; }

    }
}
