using Core.CrossCuttingConcerns.Mail;
using Core.Utilities.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace Business.Adapters.MailService
{
    public class MailServiceManager : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly MailServerOption _mailServerOption;

        public MailServiceManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailServerOption = _configuration.GetSection("MailServerOption").Get<MailServerOption>();
        }
        public async Task<bool> SendMail(string mailTo, MailBody mailBody)
        {
            return await Send(mailTo, mailBody);
        }

        private async Task<bool> Send(string mailTo,MailBody mailBody)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_mailServerOption.Username));
            email.To.Add(MailboxAddress.Parse(mailTo));
            email.Subject = mailBody.Subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = mailBody.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_mailServerOption.SmtpServer, _mailServerOption.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailServerOption.Username, _mailServerOption.Password);

            try
            {
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception)
            {
                smtp.Disconnect(true);
                return false;
            }
        }
    }
}
