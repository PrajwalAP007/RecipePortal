using Microsoft.EntityFrameworkCore;
using RecipeDataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static RecipeDataAccess.Repository.Interface.IGenericRepository;

namespace RecipeDataAccess.Repository.Implementation
{

        public class GenericRepository<T> : IGenericrepository<T> where T : class
        {
            private readonly AppDbContext _db;
            internal DbSet<T> dBSet;
            public GenericRepository(AppDbContext db)
            {
                _db = db;
                this.dBSet = db.Set<T>();
            }
            public void Add(T Entity)
            {
                _db.Add(Entity);
            }

            public void Delete(T Entity)
            {
                dBSet.Remove(Entity);
            }

            public void DeleteMutiple(IEnumerable<T> Entity)
            {
                dBSet.RemoveRange(Entity);
            }

            public IEnumerable<T> GetAll(string? includeProperties = null)
            {
                IQueryable<T> query = dBSet;
                if (includeProperties != null)
                {
                    foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(property);
                    }
                }
                return query.ToList();
            }

            public T GetById(Expression<Func<T, bool>> filter, string? includeProperties = null)
            {
                IQueryable<T> query = dBSet;
                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(property);
                    }
                }
                return query.FirstOrDefault();
            }
  
    }
    
}
