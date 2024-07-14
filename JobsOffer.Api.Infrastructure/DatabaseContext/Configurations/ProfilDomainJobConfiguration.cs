using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions;

namespace ProfilDomainJobsOffer.Api.Infrastructure.DatabaseContext.Configurations
{
    public class ProfilDomainJobConfiguration : IEntityTypeConfiguration<ProfilDomainJob>
    {
        public void Configure(EntityTypeBuilder<ProfilDomainJob> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Profil)
                   .WithMany(x => x.ProfilDomainJobs)
                   .HasForeignKey(x => x.ProfilId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.DomainJob)
                   .WithMany(x => x.ProfilDomainJobs)
                   .HasForeignKey(x => x.DomainJobId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Seed();
        }
    }
}
