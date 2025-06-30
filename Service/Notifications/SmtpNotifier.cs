using System.Net;
using System.Net.Mail;
using System.Threading;
using ProfiBotServer.Configuration;

namespace ProfiBotServer.Service.Notifications
{
    public class SmtpNotifier(SmtpNotifierSettings smtpNotifierSettings) : ISmtpNotifier
    {
        public void Send(string recipient, string subject, string body)
            => DoSend(recipient, subject, body, 3);

        void DoSend(string recipient, string subject, string body, int attemptsLeft)
        {
            try
            {
                using var client = new SmtpClient(smtpNotifierSettings.Host, smtpNotifierSettings.Port)
                {
                    Credentials = new NetworkCredential(smtpNotifierSettings.Username, smtpNotifierSettings.Password),
                    EnableSsl = true,
                    Timeout = 200000
                };

                using var message = new MailMessage(smtpNotifierSettings.Username, recipient, subject, body);

                client.Send(message);
            }
            catch (SmtpException ex) when (ex.Message.Contains("timed out"))
            {
                if (attemptsLeft <= 0)
                {
                    throw;
                }

                Thread.Sleep(3000);

                DoSend(recipient, subject, body, attemptsLeft - 1);
            }
        }
    }
}
