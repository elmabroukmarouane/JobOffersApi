using JobsOffer.Api.Business.SendEmails.Classe;
using JobsOffer.Api.Business.SendEmails.Interface;
using JobsOffer.Api.Business.Services.SendEmails.Interface;
using JobsOffer.Api.Business.Services.SendEmails.Models.Classes;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace JobsOffer.Api.Business.Services.SendEmails.Classe
{
    public class SendMailService : ISendMailService
    {
        #region ATTRIBUTES
        private readonly ISmtpClient _smtpClient;
        private readonly IEmailConfigurationFactory _emailConfigurationFactory;
        #endregion

        #region CONSTRUCTOR
        public SendMailService(ISmtpClient smtpClient, IEmailConfigurationFactory emailConfigurationFactory)
        {
            _smtpClient = smtpClient ?? throw new ArgumentException(nameof(smtpClient));
            _emailConfigurationFactory = emailConfigurationFactory ?? throw new ArgumentException(nameof(emailConfigurationFactory));
        }
        #endregion

        #region METHODS
        public async Task<string?> Send(EmailMessage? emailMessage)
        {
            if (emailMessage is not null)
            {
                if (emailMessage.ToAddresses is not null && emailMessage.ToAddresses.Any() && emailMessage.FromAddresses is not null && emailMessage.FromAddresses.Any())
                {
                    var message = new MimeMessage();
                    message.To.AddRange(emailMessage.ToAddresses?.Select(x => new MailboxAddress(x.Name, x.Address)));
                    message.From.AddRange(emailMessage.FromAddresses?.Select(x => new MailboxAddress(x.Name, x.Address)));
                    message.Subject = emailMessage.Subject;
                    var textPart = new TextPart(TextFormat.Html)
                    {
                        Text = emailMessage.Content
                    };
                    var multipart = new Multipart()
                    {
                        textPart
                    };
                    if (emailMessage.FilesPath != null)
                    {
                        if (emailMessage.FilesPath.Any())
                        {
                            foreach (var path in emailMessage.FilesPath)
                            {
                                if (!string.IsNullOrEmpty(path))
                                {
                                    var attachment = new MimePart()
                                    {
                                        Content = new MimeContent(File.OpenRead(path), ContentEncoding.Default),
                                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                                        ContentTransferEncoding = ContentEncoding.Base64,
                                        FileName = Path.GetFileName(path)
                                    };
                                    multipart.Add(attachment);
                                }
                            }
                        }
                    }
                    message.Body = multipart;
                    _smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    if (_emailConfigurationFactory is null || _emailConfigurationFactory.GetEmailConfiguration().SmtpServer is null || _emailConfigurationFactory.GetEmailConfiguration().SmtpPort <= 0) return null;
                    await _smtpClient.ConnectAsync(_emailConfigurationFactory.GetEmailConfiguration().SmtpServer, _emailConfigurationFactory.GetEmailConfiguration().SmtpPort, SecureSocketOptions.Auto);
                    //if (_emailConfigurationFactory is null || _emailConfigurationFactory.GetEmailConfiguration().SmtpUsername is null || _emailConfigurationFactory.GetEmailConfiguration().SmtpPassword is null) return null;
                    //await _smtpClient.AuthenticateAsync(_emailConfigurationFactory.GetEmailConfiguration().SmtpUsername, _emailConfigurationFactory.GetEmailConfiguration().SmtpPassword);
                    var response = await _smtpClient.SendAsync(message);
                    await _smtpClient.DisconnectAsync(true);
                    return response;
                }
            }
            return null;
        }
        #endregion
    }
}
