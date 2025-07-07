using System.Text.Json.Serialization;
using NuciDAL.DataObjects;

namespace ProfiBotServer.DataAccess.DataObjects
{
    public sealed class QrCodeEntity : EntityBase
    {
        public string Description { get; set; }

        [JsonPropertyName("enabled")]
        public bool IsEnabled { get; set; }

        [JsonPropertyName("storeName")]
        public string StoreName { get; set; }

        [JsonPropertyName("storeType")]
        public string StoreType { get; set; }
    }
}
