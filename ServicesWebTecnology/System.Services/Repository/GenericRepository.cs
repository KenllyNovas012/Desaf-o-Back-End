using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Infrastructure.IRepository;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SystemQuickzal.Contexts;

namespace System.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AplicationDataContext _context;

        public GenericRepository(AplicationDataContext context)
        {
            _context = context;
        }

        public Task<List<T>> GellAsync()
        {
            return this._context.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetAll()
        {
            return this._context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this._context.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await this._context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<T> CreateRangeAsync(IEnumerable<T> entity)
        {
            await this._context.Set<T>().AddRangeAsync(entity);
            await SaveAllAsync();
            return (T)entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //this._context.Set<T>().Update(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<List<T>> UpdateRange(List<T> entity)
        {
            //_context.Entry(entity).Status = EntityState.Modified;
            this._context.Set<T>().UpdateRange(entity);
            await SaveAllAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            this._context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }
        public async void DeleteRange(IEnumerable<T> entity)
        {
            this._context.Set<T>().RemoveRange(entity);
            await SaveAllAsync();
        }
        public void Delete(int id)
        {
            var entity = this._context.Set<T>().Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }
        public void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await this._context.SaveChangesAsync() > 0;
        }
        public bool SaveAll()
        {
            return this._context.SaveChanges() > 0;
        }

        public IQueryable<T> GetQueryable()
        {
            var myQueryable = _context.Set<T>().AsQueryable();
            return myQueryable;
        }

        public Task<bool> ExistAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<T> Pagination(int page = 1, int count = 5)
        {
            return _context.Set<T>().Skip((page - 1) * count).Take(count);
        }
        public int Count()
        {
            return _context.Set<T>().Count();
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> token)
        {
            return _context.Set<T>().Where(token);
        }
    }
}
