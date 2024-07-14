using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.DomainJob)
                   .WithMany(x => x.Jobs)
                   .HasForeignKey(x => x.IdDomainJob)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Seed();
        }
    }
}
