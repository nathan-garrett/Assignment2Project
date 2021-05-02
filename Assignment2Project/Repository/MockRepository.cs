using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Assignment2Project.Repository
{
    public class MockRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _db;

        public MockRepository(List<T> db)
        {
            _db = db;
        }

        public void Add(T entity)
        {
            _db.Add(entity);
        }

        public Task<T> Get(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null)
        {
            List<T> query = _db;

            if (filter != null)
            {
                query = query.AsQueryable().Where(filter).ToList();
            }
            await Task.Delay(1);
            return query;
        }

        public async Task<T> GetFirst(Expression<Func<T, bool>> filter = null)
        {
            List<T> query = _db;

            if (filter != null)
            {
                query = query.AsQueryable().Where(filter).ToList();
            }

            await Task.Delay(1);
            return query.AsQueryable().FirstOrDefault();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            _db.Remove(entity);
        }
    }
}
