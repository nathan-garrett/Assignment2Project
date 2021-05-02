using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Assignment2Project.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int? id);

        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null
            );

        Task<T> GetFirst(
            Expression<Func<T, bool>> filter = null
            );

        void Add(T entity);

        void Remove(int id);

        void Remove(T entity);
    }
}
