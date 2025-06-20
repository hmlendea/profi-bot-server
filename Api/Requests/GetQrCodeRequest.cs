using System.Text.Json.Serialization;
using NuciAPI.Requests;
using NuciSecurity.HMAC;

namespace ProfiBotServer.Api.Requests
{
    public class GetQrCodeRequest : Request
    {
        [HmacOrder(1)]
        [JsonPropertyName("phoneNr")]
        public string UserPhoneNumber { get; set; }
    }
}
