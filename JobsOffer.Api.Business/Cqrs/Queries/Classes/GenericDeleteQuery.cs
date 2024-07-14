using JobsOffer.Api.Business.Cqrs.Queries.Interfaces;
using JobsOffer.Api.Infrastructure.DatabaseContext.DbContextJobsOffer;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.UnitOfWork.UnitOfWork.Interface;

namespace JobsOffer.Api.Business.Cqrs.Queries.Classes
{
    public class GenericDeleteQuery<TEntity> : IGenericDeleteQuery<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly IUnitOfWork<DbContextJobsOffer> _unitOfWork;
        #endregion

        #region CONSTRUCTOR
        public GenericDeleteQuery(IUnitOfWork<DbContextJobsOffer> unitOfWork) => _unitOfWork = unitOfWork ?? throw new ArgumentException(null, nameof(unitOfWork));
        #endregion

        #region METHODS
        public async Task<TEntity?> Handle(TEntity entity)
        {
            return await _unitOfWork.GetGenericRepository<TEntity>().DeleteAsync(entity);
        }

        public async Task<IList<TEntity>?> Handle(IList<TEntity> entities)
        {
            return await _unitOfWork.GetGenericRepository<TEntity>().DeleteAsync(entities);
        }
        #endregion
    }
}
