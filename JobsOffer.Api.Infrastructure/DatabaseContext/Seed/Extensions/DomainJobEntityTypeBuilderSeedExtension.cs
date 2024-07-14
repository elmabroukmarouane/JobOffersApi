using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class DomainJobEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<DomainJob> builder)
        {
            builder.HasData(DomainJobFakeDataSeed.FakeDataDomainJobSeed(300));
        }
    }
}
