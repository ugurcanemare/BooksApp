using System.Linq.Expressions;
using BooksApp.Data.Abstract;
using BooksApp.Data.Concrete.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Data.Concrete.EfCore
{
    public class EfCoreGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        BooksAppContext _appContext = new BooksAppContext();
        public async Task CreateAsync(TEntity entity)
        {
            await _appContext.Set<TEntity>().AddAsync(entity);
            _appContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _appContext.Set<TEntity>().Remove(entity);
            _appContext.SaveChanges();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _appContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _appContext.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _appContext.Update(entity);
            _appContext.SaveChangesAsync();
        }
    }
}