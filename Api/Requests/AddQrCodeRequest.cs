using System.Text.Json.Serialization;
using NuciAPI.Requests;
using NuciSecurity.HMAC;

namespace ProfiBotServer.Api.Requests
{
    public class AddQrCodeRequest : Request
    {
        [HmacOrder(1)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [HmacOrder(2)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [HmacOrder(3)]
        [JsonPropertyName("enabled")]
        public bool IsEnabled { get; set; }

        [HmacOrder(4)]
        public string StoreType { get; set; }

        [HmacOrder(5)]
        [JsonPropertyName("phoneNr")]
        public string UserPhoneNumber { get; set; }
    }
}
