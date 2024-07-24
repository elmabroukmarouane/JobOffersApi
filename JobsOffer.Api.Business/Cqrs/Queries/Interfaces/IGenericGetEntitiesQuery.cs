using JobsOffer.Api.Infrastructure.Models.Classes;
using System.Linq.Expressions;

namespace JobsOffer.Api.Business.Cqrs.Queries.Interfaces
{
    public interface IGenericGetEntitiesQuery<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> Handle(
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orberBy = null,
            string? includes = null,
            string splitChar = ",",
            bool disableTracking = true,
            int take = 0,
            int offset = 0,
            bool inDatabase = false);
        Task<TEntity?> Handle(TEntity entity);
        IQueryable<TEntity?> Handle();
    }
}
