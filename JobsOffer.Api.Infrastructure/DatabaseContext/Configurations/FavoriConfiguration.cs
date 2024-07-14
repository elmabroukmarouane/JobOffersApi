using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions;

namespace FavorisOffer.Api.Infrastructure.DatabaseContext.Configurations
{
    public class FavoriConfiguration : IEntityTypeConfiguration<Favori>
    {
        public void Configure(EntityTypeBuilder<Favori> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Job)
                   .WithMany(x => x.Favoris)
                   .HasForeignKey(x => x.JobId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Favoris)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Seed();
        }
    }
}
