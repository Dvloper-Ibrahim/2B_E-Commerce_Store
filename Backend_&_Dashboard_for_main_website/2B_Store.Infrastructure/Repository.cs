using System.Linq.Expressions;
using System;
using _2B_Store.Application.Contracts;
using _2B_Store.Context;
using Microsoft.EntityFrameworkCore;

namespace _2B_Store.Infrastructure
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly StoreContext _dbContext;
      protected  readonly  DbSet<TEntity> _Dbset;

        public Repository(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _Dbset = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _Dbset.FindAsync(id);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return  _Dbset;
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _Dbset.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _Dbset.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _Dbset.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _Dbset.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}