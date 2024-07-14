using Bogus;
using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class ProfilFakeDataSeed
    {
        public static IList<Profil> FakeDataProfilSeed(int numberOfRows)
        {
            var id = 1;
            return new Faker<Profil>("fr")
                .RuleFor(x => x.Id, f => id++)
                .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                .RuleFor(x => x.LastName, f => f.Person.LastName)
                .RuleFor(x => x.JobTitle, f => f.Name.JobTitle())
                .RuleFor(x => x.Degree, f => f.Name.JobArea())
                .RuleFor(x => x.CreateDate, f => f.Date.Past())
                .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                .Generate(numberOfRows);

        }
    }
}
