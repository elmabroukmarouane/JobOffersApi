namespace JobsOffer.Api.Infrastructure.Models.Interfaces
{
    public interface ICommonFields
    {
        DateTime? CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        string? CreatedBy { get; set; }
        string? UpdatedBy { get; set; }
    }
}
