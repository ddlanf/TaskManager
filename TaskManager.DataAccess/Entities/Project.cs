using System;
using System.Collections.Generic;

namespace TaskManager.DataAccess.Entities
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<ProjectTask>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; } = null!;

        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }
}
