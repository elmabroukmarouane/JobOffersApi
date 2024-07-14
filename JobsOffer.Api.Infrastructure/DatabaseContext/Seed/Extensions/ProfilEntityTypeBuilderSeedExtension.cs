using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class ProfilEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<Profil> builder)
        {
            builder.HasData(ProfilFakeDataSeed.FakeDataProfilSeed(300));
        }
    }
}
