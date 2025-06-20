using NuciDAL.DataObjects;

namespace ProfiBotServer.DataAccess.DataObjects
{
    public sealed class QrCodeEntity : EntityBase<long>
    {
        public string Description { get; set; }
    }
}
