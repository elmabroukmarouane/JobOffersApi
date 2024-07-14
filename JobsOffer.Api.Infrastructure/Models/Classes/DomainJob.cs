namespace JobsOffer.Api.Infrastructure.Models.Classes
{
    public class DomainJob : Entity
    {
        public string? Domain { get; set; }
        public ICollection<Job>? Jobs { get; set; }
        public ICollection<ProfilDomainJob>? ProfilDomainJobs { get; set; }
    }
}
