namespace JobsOffer.Api.Infrastructure.Models.Classes
{
    public class Favori : Entity
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public User? User { get; set; }
        public Job? Job { get; set; }
    }
}
