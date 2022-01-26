using System;
using System.Collections.Generic;

namespace TaskManager.DataAccess.Data.Entities
{
    public partial class Project
    {
        public Project()
        {
            Employees = new HashSet<Employee>();
            Tasks = new HashSet<ProjectTask>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }
}
