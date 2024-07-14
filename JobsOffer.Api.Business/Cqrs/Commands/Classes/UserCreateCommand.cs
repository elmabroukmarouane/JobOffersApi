using JobsOffer.Api.Business.Cqrs.Commands.Interfaces;
using JobsOffer.Api.Business.Helpers;
using JobsOffer.Api.Infrastructure.DatabaseContext.DbContextJobsOffer;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.UnitOfWork.UnitOfWork.Interface;

namespace JobsOffer.Api.Business.Cqrs.Commands.Classes
{
    public class UserCreateCommand : IUserCreateCommand
    {
        #region ATTRIBUTES
        protected readonly IUnitOfWork<DbContextJobsOffer> _unitOfWork;
        #endregion

        #region CONTRUCTOR
        public UserCreateCommand(IUnitOfWork<DbContextJobsOffer> unitOfWork) => _unitOfWork = unitOfWork ?? throw new ArgumentException(null, nameof(unitOfWork));
        #endregion

        #region HANDLE
        public async Task<User?> Handle(User entity)
        {
            if (entity is null) return entity;
            entity = Helper.EncryptPassword(entity);
            await _unitOfWork.GetGenericRepository<User>().CreateAsync(entity);
            await _unitOfWork.Save();
            return entity;
        }

        public async Task<IList<User>?> Handle(IList<User> entities)
        {
            if (entities is null || !entities.Any()) return entities;
            entities = Helper.EncryptPassword(entities);
            await _unitOfWork.GetGenericRepository<User>().CreateAsync(entities);
            await _unitOfWork.Save();
            return entities;
        }
        #endregion
    }
}
