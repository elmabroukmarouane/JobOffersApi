namespace JobsOffer.Api.Infrastructure.Models.Classes
{
    public class ProfilDomainJob : Entity
    {
        public int ProfilId { get; set; }
        public int DomainJobId { get; set; }
        public Profil? Profil { get; set; }
        public DomainJob? DomainJob { get; set; }
    }
}
