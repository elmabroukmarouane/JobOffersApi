using JobsOffer.Api.Infrastructure.Models.Classes;
using System.Linq.Expressions;

namespace JobsOffer.Api.Business.Services.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : Entity
    {
        #region READ
        IQueryable<TEntity> GetEntitiesAsync(
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orberBy = null,
            string? includes = null,
            string splitChar = ",",
            bool disableTracking = true,
            int take = 0,
            int offset = 0,
            bool inDatabase = false);
        Task<TEntity?> GetEntitiesAsync(TEntity entity);
        #endregion

        #region CREATE
        Task<TEntity?> CreateAsync(TEntity entity);
        Task<IList<TEntity>?> CreateAsync(IList<TEntity> entities);
        #endregion

        #region UPDATE
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<IList<TEntity>?> UpdateAsync(IList<TEntity> entities);
        #endregion

        #region DELETE
        Task<TEntity?> DeleteAsync(TEntity entity);
        Task<IList<TEntity>?> DeleteAsync(IList<TEntity> entities);
        #endregion
    }
}
