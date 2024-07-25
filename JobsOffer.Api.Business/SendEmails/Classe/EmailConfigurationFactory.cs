using JobsOffer.Api.Business.SendEmails.Interface;
using JobsOffer.Api.Business.Services.SendEmails.Models.Classes;

namespace JobsOffer.Api.Business.SendEmails.Classe
{
    public class EmailConfigurationFactory : IEmailConfigurationFactory
    {
        #region ATTRIBUTES
        protected readonly EmailConfiguration _emailConfiguration;
        #endregion

        #region CONSTRUCTOR
        public EmailConfigurationFactory(EmailConfiguration emailConfiguration) => _emailConfiguration = emailConfiguration ?? throw new ArgumentException(null, nameof(emailConfiguration));
        #endregion

        #region METHODS
        public EmailConfiguration GetEmailConfiguration() => _emailConfiguration;
        #endregion
    }
}
