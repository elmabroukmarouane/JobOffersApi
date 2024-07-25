using JobsOffer.Api.Business.SendEmails.Classe;
using JobsOffer.Api.Business.SendEmails.Interface;
using JobsOffer.Api.Business.Services.SendEmails.Models.Classes;

namespace JobsOffer.Api.Server.Extensions.Add;
public static class AddEmailSmtpConfiguration
{
    public static void AddEmailSmtpConfigurationExtension(this IServiceCollection self, IConfiguration configuration)
    {
        var smtpServer = configuration.GetSection("EmailConfiguration:SmtpServer").Value;
        var smtpPort = int.Parse(configuration.GetSection("EmailConfiguration:SmtpPort").Value!);
        var smtpUsername = configuration.GetSection("EmailConfiguration:SmtpUsername").Value;
        var smtpPassword = configuration.GetSection("EmailConfiguration:SmtpPassword").Value;
        var emailSmtpConfiguration = new EmailConfiguration()
        {
            SmtpServer = smtpServer,
            SmtpPort = smtpPort,
            SmtpUsername = smtpUsername,
            SmtpPassword = smtpPassword,
        };
        self.AddSingleton<IEmailConfigurationFactory>(new EmailConfigurationFactory(emailSmtpConfiguration));
    }
}
