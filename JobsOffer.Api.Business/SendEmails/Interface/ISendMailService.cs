using JobsOffer.Api.Business.Services.SendEmails.Models.Classes;

namespace JobsOffer.Api.Business.Services.SendEmails.Interface;
public interface ISendMailService
{
    Task<string?> Send(EmailMessage? emailMessage);
}
