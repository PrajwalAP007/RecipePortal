using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDataAccess.Repository.Interface
{
    public interface IGenericRepository
    {
        public interface IGenericrepository<T> where T : class
        {
            IEnumerable<T> GetAll(string? includeProperties = null);
            T GetById(Expression<Func<T, bool>> Filter, string? includeProperties = null);

            void Add(T Entity);
            void Delete(T Entity);
            void DeleteMutiple(IEnumerable<T> entity);

        }
    }
}
