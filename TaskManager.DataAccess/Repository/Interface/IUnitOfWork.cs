using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Data.Entities;

namespace TaskManager.DataAccess.Repository.Interface
{
    public interface IUnitOfWork
    {
        public ITaskDataAccess ProjectTasks { get; }
        public IEmployeeDataAccess Employees { get; }
        public IProjectDataAccess Projects { get; }
        void Save();
    }
}
