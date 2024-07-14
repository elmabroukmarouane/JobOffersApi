using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class FavoriEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<Favori> builder)
        {
            builder.HasData(FavoriFakeDataSeed.FakeDataFavoriSeed(300));
        }
    }
}
