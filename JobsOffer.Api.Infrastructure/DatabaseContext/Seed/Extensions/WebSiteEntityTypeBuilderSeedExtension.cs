using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class WebSiteEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<WebSite> builder)
        {
            builder.HasData(WebSiteFakeDataSeed.FakeDataWebSiteSeed(300));
        }
    }
}
