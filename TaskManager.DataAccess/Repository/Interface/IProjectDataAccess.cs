using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Data.Entities;

namespace TaskManager.DataAccess.Repository.Interface
{
    public interface IProjectDataAccess : ITaskManagerDataAccess<Project>
    {
        IEnumerable<Project> GetProjectsWithStoredProcedure();
    }
}
