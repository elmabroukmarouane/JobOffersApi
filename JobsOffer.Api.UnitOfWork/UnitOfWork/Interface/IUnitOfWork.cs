using JobsOffer.Api.Domain.GenericRepository.Interface;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace JobsOffer.Api.UnitOfWork.UnitOfWork.Interface
{
    public interface IUnitOfWork<TDbContext> : IDisposable 
        where TDbContext : DbContext 
    {
        TDbContext DbContext { get; }
        IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : Entity;
        Task<int> Save();
    }
}
