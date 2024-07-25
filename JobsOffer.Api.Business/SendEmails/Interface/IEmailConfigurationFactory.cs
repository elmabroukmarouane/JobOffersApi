using JobsOffer.Api.Business.Services.SendEmails.Models.Classes;

namespace JobsOffer.Api.Business.SendEmails.Interface
{
    public interface IEmailConfigurationFactory
    {
        EmailConfiguration GetEmailConfiguration();
    }
}
