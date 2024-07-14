using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Business.Cqrs.Commands.Interfaces
{
    public interface IUserUpdateCommand
    {
        Task<User?> Handle(User entity, bool updateOnLineAuthentication = false);
        Task<IList<User>?> Handle(IList<User> entities);
    }
}
