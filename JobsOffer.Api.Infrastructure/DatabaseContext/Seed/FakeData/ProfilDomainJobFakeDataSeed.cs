using Bogus;
using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class ProfilDomainJobFakeDataSeed
    {
        public static IList<ProfilDomainJob> FakeDataProfilDomainJobSeed(int numberOfRows)
        {
            var id = 1;
            var profilIds = ProfilFakeDataSeed.FakeDataProfilSeed(300).Select(p => p.Id).ToList();
            var domainJobIds = DomainJobFakeDataSeed.FakeDataDomainJobSeed(300).Select(p => p.Id).ToList();
            return new Faker<ProfilDomainJob>("fr")
                .RuleFor(x => x.Id, f => id++)
                .RuleFor(x => x.ProfilId, f => f.PickRandom(profilIds))
                .RuleFor(x => x.DomainJobId, f => f.PickRandom(domainJobIds))
                .RuleFor(x => x.CreateDate, f => f.Date.Past())
                .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                .Generate(numberOfRows);

        }
    }
}
