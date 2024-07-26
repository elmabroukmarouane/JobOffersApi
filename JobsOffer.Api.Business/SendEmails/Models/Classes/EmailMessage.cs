namespace JobsOffer.Api.Business.Services.SendEmails.Models.Classes;
public class EmailMessage
{
    public required IList<EmailAddress> ToAddresses { get; set; }
    public required IList<EmailAddress> FromAddresses { get; set; }
    public string? Subject { get; set; }
    public string? Content { get; set; }
    public IList<string?>? FilesPath { get; set; }
}
