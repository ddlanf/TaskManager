using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.DataAccess.Data.Entities
{
    [Table(name:"Tasks")]
    public partial class ProjectTask
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public int? ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}
