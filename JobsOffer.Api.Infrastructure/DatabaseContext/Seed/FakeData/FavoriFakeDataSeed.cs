using Bogus;
using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class FavoriFakeDataSeed
    {
        public static IList<Favori> FakeDataFavoriSeed(int numberOfRows)
        {
            var id = 1;
            var userIds = UserFakeDataSeed.FakeDataUserSeed(300).Select(p => p.Id).ToList();
            var jobIds = JobFakeDataSeed.FakeDataJobSeed(300).Select(p => p.Id).ToList();
            return new Faker<Favori>("fr")
                .RuleFor(x => x.Id, f => id++)
                .RuleFor(x => x.UserId, f => f.PickRandom(userIds))
                .RuleFor(x => x.JobId, f => f.PickRandom(jobIds))
                .RuleFor(x => x.CreateDate, f => f.Date.Past())
                .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                .Generate(numberOfRows);

        }
    }
}
