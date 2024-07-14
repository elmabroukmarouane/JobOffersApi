namespace JobsOffer.Api.Infrastructure.Models.Classes
{
    public class Profil : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public string? Degree { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<ProfilDomainJob>? ProfilDomainJobs { get; set; }
    }
}
