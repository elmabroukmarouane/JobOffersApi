using Bogus;
using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData
{
    public static class WebSiteFakeDataSeed
    {
        public static IList<WebSite> FakeDataWebSiteSeed(int numberOfRows)
        {
            var id = 1;
            return new Faker<WebSite>("fr")
                .RuleFor(x => x.Id, f => id++)
                .RuleFor(x => x.SiteName, f => f.Company.CompanyName())
                .RuleFor(x => x.SiteUrl, f => f.Lorem.Word())
                .RuleFor(x => x.CreateDate, f => f.Date.Past())
                .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                .Generate(numberOfRows);

        }
    }
}
