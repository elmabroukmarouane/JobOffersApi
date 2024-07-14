using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Server.DtoModel.Models
{
    public class ProfilDomainJobViewModel : Entity
    {
        public int ProfilId { get; set; }
        public int DomainJobId { get; set; }
        public ProfilViewModel? Profil { get; set; }
        public DomainJobViewModel? DomainJob { get; set; }
    }
}
