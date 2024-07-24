using JobsOffer.Api.Infrastructure.Models.Classes;
using System.Linq.Expressions;

namespace JobsOffer.Api.Business.Services.Interfaces
{
    public interface IUserService
    {
        #region READ
        IQueryable<User> GetEntitiesAsync(
            Expression<Func<User, bool>>? expression = null,
            Func<IQueryable<User>, IOrderedQueryable<User>>? orberBy = null,
            string? includes = null,
            string splitChar = ",",
            bool disableTracking = true,
            int take = 0,
            int offset = 0,
            bool inDatabase = false);
        Task<User?> GetEntitiesAsync(User entity);
        #endregion

        #region CREATE
        Task<User?> CreateAsync(User entity);
        Task<IList<User>?> CreateAsync(IList<User> entities);
        #endregion

        #region UPDATE
        Task<User?> UpdateAsync(User entity);
        Task<IList<User>?> UpdateAsync(IList<User> entities);
        #endregion

        #region DELETE
        Task<User?> DeleteAsync(User entity);
        Task<IList<User>?> DeleteAsync(IList<User> entities);
        #endregion

        #region AUTENTICATION
        Task<User?> Authenticate(User user, bool inDatabase = false);
        Task<bool> Logout(User user, bool inDatabase = false);
        #endregion

        #region TOKEN
        string? CreateToken(object user, string keyString, string issuerString, string audienceString, int expireTokenDays = 1);
        #endregion
    }
}
