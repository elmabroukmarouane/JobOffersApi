using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Business.Cqrs.Commands.Interfaces
{
    public interface IUserCreateCommand
    {
        Task<User?> Handle(User entity);
        Task<IList<User>?> Handle(IList<User> entities);
    }
}
