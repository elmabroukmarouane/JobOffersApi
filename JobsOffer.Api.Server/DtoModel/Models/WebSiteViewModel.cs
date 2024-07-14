using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Server.DtoModel.Models
{
    public class WebSiteViewModel : Entity
    {
        public string? SiteName { get; set; }
        public string? SiteUrl { get; set; }

    }
}
