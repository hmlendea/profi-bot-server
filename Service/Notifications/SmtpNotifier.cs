using System.Net;
using System.Net.Mail;
using ProfiBotServer.Configuration;

namespace ProfiBotServer.Service.Notifications
{
    public class SmtpNotifier(SmtpNotifierSettings smtpNotifierSettings) : ISmtpNotifier
    {
        public void Send(string recipient, string subject, string body)
        {
            using var client = new SmtpClient(smtpNotifierSettings.Host, smtpNotifierSettings.Port)
            {
                Credentials = new NetworkCredential(smtpNotifierSettings.Username, smtpNotifierSettings.Password),
                EnableSsl = true
            };

            using var message = new MailMessage(smtpNotifierSettings.Username, recipient, subject, body);

            client.Send(message);
        }
    }
}
