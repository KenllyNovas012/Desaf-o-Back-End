using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        void Delete(int id);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
        bool SaveAll();
        IEnumerable<T> FindAll(Expression<Func<T, bool>> token);

        Task<List<T>> UpdateRange(List<T> entity);

        Task<T> CreateRangeAsync(IEnumerable<T> entity);

        IQueryable<T> GetQueryable();

        Task<List<T>> GellAsync();

        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);


        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(Guid id);
        IEnumerable<T> Pagination(int page = 1, int count = 5);
        int Count();

    }
}
