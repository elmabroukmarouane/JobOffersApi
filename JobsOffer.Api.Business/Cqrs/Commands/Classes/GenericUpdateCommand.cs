using JobsOffer.Api.Business.Cqrs.Commands.Interfaces;
using JobsOffer.Api.Infrastructure.DatabaseContext.DbContextJobsOffer;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.UnitOfWork.UnitOfWork.Interface;

namespace JobsOffer.Api.Business.Cqrs.Commands.Classes
{
    public class GenericUpdateCommand<TEntity> : IGenericUpdateCommand<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly IUnitOfWork<DbContextJobsOffer> _unitOfWork;
        #endregion

        #region CONTRUCTOR
        public GenericUpdateCommand(IUnitOfWork<DbContextJobsOffer> unitOfWork) => _unitOfWork = unitOfWork ?? throw new ArgumentException(null, nameof(unitOfWork));
        #endregion

        #region HANDLE
        public async Task<TEntity?> Handle(TEntity entity)
        {
            if (entity is null) return entity;
            await _unitOfWork.GetGenericRepository<TEntity>().UpdateAsync(entity);
            await _unitOfWork.Save();
            return entity;
        }

        public async Task<IList<TEntity>?> Handle(IList<TEntity> entities)
        {
            if (entities is null || !entities.Any()) return entities;
            await _unitOfWork.GetGenericRepository<TEntity>().UpdateAsync(entities);
            await _unitOfWork.Save();
            return entities;
        }
        #endregion
    }
}
