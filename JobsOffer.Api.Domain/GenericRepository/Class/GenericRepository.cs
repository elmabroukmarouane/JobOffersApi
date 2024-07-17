using JobsOffer.Api.Domain.GenericRepository.Interface;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JobsOffer.Api.Domain.GenericRepository.Class
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        #endregion

        #region CONSTRUCTOR
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(null, nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }
        #endregion

        #region CREATE
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity: entity);
            return entity;
        }

        public virtual async Task<IList<TEntity>> CreateAsync(IList<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities: entities);
            return entities;
        }
        #endregion

        #region DELETE
        public virtual async Task<TEntity?> DeleteAsync(TEntity entity)
        {
            var entityToDelete = await _dbSet.FindAsync(entity.Id);
            if(entityToDelete != null)
            {
                _dbSet.Remove(entity: entityToDelete);
                await _dbContext.SaveChangesAsync();
            }
            return entityToDelete;
        }

        public virtual async Task<IList<TEntity>?> DeleteAsync(IList<TEntity> entities)
        {
            _dbSet.RemoveRange(entities: entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }
        #endregion

        #region READ
        public virtual IQueryable<TEntity> GetEntitiesAsync(
            Expression<Func<TEntity, bool>>? expression = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orberBy = null, 
            string? includes = null,
            string splitChar = ",", 
            bool disableTracking = true, 
            int take = 0, 
            int offset = 0)
        {
            var queryableEntity = (IQueryable<TEntity>)_dbSet;

            if (expression != null)
            {
                queryableEntity = queryableEntity.Where(expression);
            }

            if (orberBy != null)
            {
                queryableEntity = orberBy(queryableEntity);
            }

            if (includes != null)
            {
                foreach (var include in includes.Split(splitChar, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryableEntity = queryableEntity.Include(include);
                }
            }

            if (disableTracking)
            {
                queryableEntity = queryableEntity.AsNoTracking();
            }

            if (take > 0)
            {
                queryableEntity = queryableEntity.Take(take);
            }

            if (offset > 0)
            {
                queryableEntity = queryableEntity.Skip(offset);
            }

            return queryableEntity;
        }

        public virtual async Task<TEntity?> GetEntitiesAsync(TEntity entity) => await _dbSet.FindAsync(entity.Id);
        #endregion

        #region UPDATE
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IList<TEntity>> UpdateAsync(IList<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }
        #endregion
    }
}
