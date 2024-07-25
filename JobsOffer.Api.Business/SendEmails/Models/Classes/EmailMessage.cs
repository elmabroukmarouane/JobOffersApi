namespace JobsOffer.Api.Business.Services.SendEmails.Models.Classes;
public class EmailMessage
{
    public IList<EmailAddress>? ToAddresses { get; set; }
    public IList<EmailAddress>? FromAddresses { get; set; }
    public string? Subject { get; set; }
    public string? Content { get; set; }
    public IList<string?>? FilesPath { get; set; }
}
