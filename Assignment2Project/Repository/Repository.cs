using Assignment2Project.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Assignment2Project.Repository
{
    
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly ApplicationDbContext _db;
            private DbSet<T> dbSet;

            public Repository(ApplicationDbContext db)
            {
                _db = db;
                dbSet = _db.Set<T>();
            }

            public void Add(T entity)
            {
                dbSet.Add(entity);
            }

            public async Task<T> Get(int? id)
            {
                return await dbSet.FindAsync(id);
            }

            public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null)
            {
                IQueryable<T> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.ToListAsync();
            }

            public async Task<T> GetFirst(Expression<Func<T, bool>> filter = null)
            {
                IQueryable<T> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.FirstOrDefaultAsync();
            }

            public void Remove(int id)
            {
                T entity = dbSet.Find(id);
                Remove(entity);
            }

            public void Remove(T entity)
            {
                dbSet.Remove(entity);
            }
        }
}

