using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Profil)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.ProfilId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.Email)
                   .IsUnique();
            builder.Seed();
        }
    }
}
