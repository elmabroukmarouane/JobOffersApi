using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.Extensions;
using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsOffer.Api.Infrastructure.DatabaseContext.Configurations
{
    public class WebSiteConfiguration : IEntityTypeConfiguration<WebSite>
    {
        public void Configure(EntityTypeBuilder<WebSite> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Seed();
        }
    }
}
