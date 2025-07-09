using NuciDAL.DataObjects;

namespace ProfiBotServer.DataAccess.DataObjects
{
    public sealed class UserEntity : EntityBase
    {
        public string Password { get; set; }

        public string Description { get; set; }

        public string SmtpNotificationRecipient { get; set; }
    }
}
