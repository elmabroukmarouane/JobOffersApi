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
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (User)obj;
            return Id == other.Id && Email == other.Email;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ (Email?.GetHashCode() ?? 0);
        }
    }
}
