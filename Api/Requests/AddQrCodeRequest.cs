using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NuciAPI.Requests;
using NuciSecurity.HMAC;

namespace ProfiBotServer.Api.Requests
{
    public class AddQrCodeRequest : NuciApiRequest
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

        [Required]
        [HmacOrder(4)]
        [JsonPropertyName("storeName")]
        public string StoreName { get; set; }

        [Required]
        [HmacOrder(5)]
        [JsonPropertyName("storeType")]
        public string StoreType { get; set; }

        [HmacOrder(6)]
        [JsonPropertyName("phoneNr")]
        public string UserPhoneNumber { get; set; }
    }
}
