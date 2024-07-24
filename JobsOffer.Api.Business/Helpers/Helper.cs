using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public static List<AttributeType> GetAttributeTypes(Type parentType)
        {
            var attributeTypes = new HashSet<AttributeType>();
            var properties = parentType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var property in properties)
            {
                var typeProperty = property.PropertyType;
                if (typeProperty.IsGenericType && typeProperty.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    var elementType = typeProperty.GetGenericArguments()[0];
                    attributeTypes.Add(new AttributeType()
                    {
                        PropertyType = elementType,
                        TypeClass = TypeClass.Collection
                    });
                }
                else if (typeof(Entity).IsAssignableFrom(typeProperty))
                {
                    attributeTypes.Add(new AttributeType()
                    {
                        PropertyType = typeProperty,
                        TypeClass = TypeClass.Class
                    });
                }
                else
                {
                    attributeTypes.Add(new AttributeType()
                    {
                        PropertyType = typeProperty,
                        TypeClass = TypeClass.Other
                    });
                }
            }
            return attributeTypes.ToList();
        }

    }

    public class AttributeType
    {
        public required Type PropertyType { get; set; }
        public required TypeClass TypeClass { get; set; }
    }

    public enum TypeClass
    {
        Collection,
        Class,
        Other
    }
}
