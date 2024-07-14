using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Business.Cqrs.Commands.Interfaces
{
    public interface IGenericUpdateCommand<TEntity> where TEntity : Entity
    {
        Task<TEntity?> Handle(TEntity entity);
        Task<IList<TEntity>?> Handle(IList<TEntity> entities);
    }
}
