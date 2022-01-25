using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Data.Entities;

namespace TaskManager.DataAccess.Repository.Interface
{
    public interface ITaskManagerDataAccess<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        void Add(T item);
    }
}
