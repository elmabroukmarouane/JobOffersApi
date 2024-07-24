using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

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

        public static List<Type> GetAttributeTypes(Type parentType)
        {
            var attributeTypes = new HashSet<Type>();
            var properties = parentType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    var elementType = property.PropertyType.GetGenericArguments()[0];
                    attributeTypes.Add(elementType);
                }
                else
                {
                    attributeTypes.Add(property.PropertyType);
                }
            }
            return attributeTypes.ToList();
        }
    }
}
