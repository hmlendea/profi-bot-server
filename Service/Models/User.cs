namespace ProfiBotServer.Service.Models
{
    public sealed class User
    {
        public string Id { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }

        public string SmtpNotificationRecipient { get; set; }
    }
}
