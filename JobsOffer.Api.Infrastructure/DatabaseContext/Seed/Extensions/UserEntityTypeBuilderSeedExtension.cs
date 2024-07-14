using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class UserEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<User> builder)
        {
            builder.HasData(UserFakeDataSeed.FakeDataUserSeed(300));
        }
    }
}
