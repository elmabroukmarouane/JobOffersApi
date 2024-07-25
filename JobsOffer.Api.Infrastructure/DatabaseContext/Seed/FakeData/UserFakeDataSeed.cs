using Bogus;
using JobsOffer.Api.Infrastructure.Models.Classes;
using System.Security.Cryptography;
using System.Text;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class UserFakeDataSeed
    {
        public static IList<User> FakeDataUserSeed(int numberOfRows)
        {
            var id = 1;
            var profilIds = ProfilFakeDataSeed.FakeDataProfilSeed(300).Select(p => p.Id).ToList();
            return new Faker<User>("fr")
                .RuleFor(x => x.Id, f => id++)
                .RuleFor(x => x.ProfilId, f => f.PickRandom(profilIds))
                .RuleFor(x => x.Email, f =>
                {
                    return "user" + (id - 1) + "@test.com";
                })
                .RuleFor(x => x.Password, f =>
                {
                    return CreateHashPassword("123456");
                })
                .RuleFor(x => x.CreateDate, f => f.Date.Past())
                .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                .RuleFor(x => x.IsOnLine, f => id % 2 == 0)
                .Generate(numberOfRows);

        }

        public static string? CreateHashPassword(string? password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using (var sha512Hash = SHA512.Create())
            {
                var bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(password.Trim()));
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
