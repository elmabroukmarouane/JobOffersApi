using Bogus;
using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class DomainJobFakeDataSeed
    {
        public static IList<DomainJob> FakeDataDomainJobSeed(int numberOfRows)
        {
            var id = 1;
            return new Faker<DomainJob>("fr")
                .RuleFor(x => x.Id, f => id++)
                .RuleFor(x => x.Domain, f => f.Lorem.Word())
                .RuleFor(x => x.CreateDate, f => f.Date.Past())
                .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                .Generate(numberOfRows);

        }
    }
}
