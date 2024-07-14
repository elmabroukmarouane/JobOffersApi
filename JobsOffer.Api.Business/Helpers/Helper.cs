using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Business.Helpers
{
    public static class Helper
    {
        public static IList<int> SplitStringToListInt(string stringToSplit)
        {
            var ids = stringToSplit.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var idsList = new List<int>();
            foreach (var id in ids)
            {
                bool isParsableId = int.TryParse(id, out int idInt);
                if (isParsableId)
                {
                    idsList.Add(idInt);
                }
            }
            return idsList;
        }

        public static User EncryptPassword(User user)
        {
            user.Password = UserFakeDataSeed.CreateHashPassword(user.Password);
            return user;
        }

        public static IList<User> EncryptPassword(IList<User> users)
        {
            foreach (var user in users)
            {
                user.Password = UserFakeDataSeed.CreateHashPassword(user.Password);
            }
            return users;
        }
    }
}
