using System.Text.Json.Serialization;
using NuciAPI.Requests;
using NuciSecurity.HMAC;

namespace ProfiBotServer.Api.Requests
{
    public class RecordPrizeRequest : Request
    {
        [HmacOrder(1)]
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [HmacOrder(2)]
        [JsonPropertyName("phoneNr")]
        public string UserPhoneNumber { get; set; }
    }
}
