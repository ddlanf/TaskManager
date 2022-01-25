using System;
using System.Collections.Generic;

namespace TaskManager.BusinessLogic.Models
{
    public partial class ProjectModel
    {
        public int? Id { get; set; }
        public string ProjectName { get; set; } = null!;

        public ICollection<ProjectTaskModel>? Tasks { get; set; }
    }
}
