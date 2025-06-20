using NuciAPI.Responses;
using NuciSecurity.HMAC;

namespace ProfiBotServer.Api.Responses
{
    public class GetQrCodeResponse : SuccessResponse
    {
        [HmacOrder(1)]
        public string Id { get; set; }

        [HmacIgnore]
        public string Url { get; set; }
    }
}
