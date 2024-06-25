using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using UserProduct.Core.Abstractions;
using UserProduct.Domain.Entities;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace UserProduct.Core.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSenderService(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
            if (string.IsNullOrEmpty(_emailConfig.SenderEmail))
            {
                throw new ArgumentException("Sender email address cannot be empty.", nameof(_emailConfig.SenderEmail));
            }
        }


        public async Task SendEmailAsync(string to, string subject, string content)
        {
            // Validate the toEmail parameter
            if (string.IsNullOrEmpty(to)) 
                throw new ArgumentException("Receiver's email address is required.", nameof(to));

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.SenderEmail));

            try
            {
                email.To.Add(MailboxAddress.Parse(to));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid to email address format.", nameof(to), ex);
            }

            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = content };

            using (var smtp = new SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(_emailConfig.MailServer, _emailConfig.MailPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
                    await smtp.SendAsync(email);
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    throw new InvalidOperationException("Failed to send email", ex);
                }
                finally
                {
                    await smtp.DisconnectAsync(true);
                }
            }
        }
    }
}
