using NuciDAL.DataObjects;

namespace ProfiBotServer.DataAccess.DataObjects
{
    public sealed class PrizeEntity : EntityBase
    {
        public string UserId { get; set; }

        public string Timestamp { get; set; }
    }
}
