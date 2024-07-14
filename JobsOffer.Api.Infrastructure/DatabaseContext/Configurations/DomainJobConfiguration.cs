using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Configurations
{
    public class DomainJobConfiguration : IEntityTypeConfiguration<DomainJob>
    {
        public void Configure(EntityTypeBuilder<DomainJob> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Seed();
        }
    }
}
