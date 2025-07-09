using System.Text.Json.Serialization;
using NuciAPI.Responses;
using NuciSecurity.HMAC;

namespace ProfiBotServer.Api.Responses
{
    public class GetUserBalanceResponse : NuciApiSuccessResponse
    {
        [HmacOrder(1)]
        [JsonPropertyName("phoneNr")]
        public string UserPhoneNumber { get; set; }

        [HmacOrder(2)]
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
