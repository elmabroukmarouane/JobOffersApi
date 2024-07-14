namespace JobsOffer.Api.Infrastructure.Models.Classes
{
    public class User : Entity
    {
        public int ProfilId { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public bool IsOnLine { get; set; }
        public Profil? Profil { get; set; }
        public ICollection<Favori>? Favoris { get; set; }
    }
}
