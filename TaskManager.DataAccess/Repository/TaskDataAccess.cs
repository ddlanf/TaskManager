using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.Data.Entities;
using TaskManager.DataAccess.Repository.Interface;

namespace TaskManager.DataAccess.Repository
{
    public class TaskDataAccess : TaskManagerDataAccess<ProjectTask>, ITaskDataAccess
    {
        private readonly TaskManagerContext _db;
        public TaskDataAccess(TaskManagerContext db) : base(db)
        {
            _db = db;
        }
    }
}
