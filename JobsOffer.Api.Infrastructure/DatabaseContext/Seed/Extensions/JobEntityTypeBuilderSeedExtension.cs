using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class JobEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<Job> builder)
        {
            builder.HasData(JobFakeDataSeed.FakeDataJobSeed(300));
        }
    }
}
