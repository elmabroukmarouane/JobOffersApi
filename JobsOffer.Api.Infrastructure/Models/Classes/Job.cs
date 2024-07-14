namespace JobsOffer.Api.Infrastructure.Models.Classes
{
    public class Job : Entity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? Image { get; set; }
        public int IdDomainJob { get; set; }
        public DomainJob? DomainJob { get; set; } 
        public ICollection<Favori>? Favoris { get; set; }
    }
}
