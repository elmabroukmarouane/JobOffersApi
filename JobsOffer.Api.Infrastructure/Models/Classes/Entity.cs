using JobsOffer.Api.Infrastructure.Models.Interfaces;

namespace JobsOffer.Api.Infrastructure.Models.Classes
{
    public class Entity : IIds, ICommonFields
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
