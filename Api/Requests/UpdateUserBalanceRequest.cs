using System.Text.Json.Serialization;
using NuciAPI.Requests;
using NuciSecurity.HMAC;

namespace ProfiBotServer.Api.Requests
{
    public class UpdateUserBalanceRequest : NuciApiRequest
    {
        [HmacOrder(1)]
        [JsonPropertyName("phoneNr")]
        public string UserPhoneNumber { get; set; }

        [HmacOrder(2)]
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
