using JobsOffer.Api.Business.Cqrs.Queries.Interfaces;
using JobsOffer.Api.Infrastructure.DatabaseContext.DbContextJobsOffer;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.UnitOfWork.UnitOfWork.Interface;
using System.Linq.Expressions;

namespace JobsOffer.Api.Business.Cqrs.Queries.Classes
{
    public class GenericGetEntitiesQuery<TEntity> : IGenericGetEntitiesQuery<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        protected readonly IUnitOfWork<DbContextJobsOffer> _unitOfWork;
        #endregion

        #region CONSTRUCTOR
        public GenericGetEntitiesQuery(IUnitOfWork<DbContextJobsOffer> unitOfWork) => _unitOfWork = unitOfWork ?? throw new ArgumentException(null , nameof(unitOfWork));
        #endregion

        #region METHODS

        public IQueryable<TEntity> Handle(
            Expression<Func<TEntity, bool>>? expression = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orberBy = null, 
            string? includes = null, 
            string splitChar = ",", 
            bool disableTracking = true, 
            int take = 0, 
            int offset = 0) => _unitOfWork.GetGenericRepository<TEntity>().GetEntitiesAsync(expression, orberBy, includes, splitChar, disableTracking, take, offset);

        public async Task<TEntity?> Handle(TEntity entity) => await _unitOfWork.GetGenericRepository<TEntity>().GetEntitiesAsync(entity);
        #endregion
    }
}
