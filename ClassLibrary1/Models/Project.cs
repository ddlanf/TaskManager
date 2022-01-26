using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
