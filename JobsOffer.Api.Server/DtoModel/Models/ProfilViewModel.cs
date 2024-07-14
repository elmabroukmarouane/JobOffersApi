using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Server.DtoModel.Models
{
    public class ProfilViewModel : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public string? Degree { get; set; }
        public ICollection<UserViewModel>? Users { get; set; }
        public ICollection<ProfilDomainJobViewModel>? ProfilDomainJobs { get; set; }
    }
}
