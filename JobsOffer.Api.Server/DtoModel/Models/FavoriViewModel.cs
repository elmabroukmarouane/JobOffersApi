using JobsOffer.Api.Infrastructure.Models.Classes;

namespace JobsOffer.Api.Server.DtoModel.Models
{
    public class FavoriViewModel : Entity
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public UserViewModel? User { get; set; }
        public JobViewModel? Job { get; set; }
    }
}
