using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class ProfilDomainJobEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<ProfilDomainJob> builder)
        {
            builder.HasData(ProfilDomainJobFakeDataSeed.FakeDataProfilDomainJobSeed(300));
        }
    }
}
