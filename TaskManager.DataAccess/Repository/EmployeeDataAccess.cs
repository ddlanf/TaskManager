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
    public  class EmployeeDataAccess : TaskManagerDataAccess<Employee>, IEmployeeDataAccess
    {
        private readonly TaskManagerContext _db;
        public EmployeeDataAccess(TaskManagerContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Employee> GetEmployeesByProjectIdWithStoredProcedure(int id)
        {
            return _db.Employees.FromSqlInterpolated($"EXECUTE {StoredProcedures.GetEmployeesByProjectId} @Id={id}").ToList();
        }
    }
}
