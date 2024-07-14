using Bogus;
using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class JobFakeDataSeed
    {
        public static IList<Job> FakeDataJobSeed(int numberOfRows)
        {
            var id = 1;
            var domainJobIds = DomainJobFakeDataSeed.FakeDataDomainJobSeed(300).Select(p => p.Id).ToList();
            return new Faker<Job>("fr")
                .RuleFor(x => x.Id, f => id++)
                .RuleFor(x => x.IdDomainJob, f => f.PickRandom(domainJobIds))
                .RuleFor(x => x.Title, f => f.Name.JobTitle())
                .RuleFor(x => x.Description, f => f.Name.JobDescriptor())
                .RuleFor(x => x.Link, f => f.Internet.Url())
                .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                .RuleFor(x => x.CreateDate, f => f.Date.Past())
                .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                .Generate(numberOfRows);

        }
    }
}
