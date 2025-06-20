namespace ProfiBotServer.Service.Notifications
{
    public interface ISmtpNotifier
    {
        void Send(string recipient, string subject, string body);
    }
}
