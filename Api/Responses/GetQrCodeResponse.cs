using NuciAPI.Responses;
using NuciSecurity.HMAC;

namespace ProfiBotServer.Api.Responses
{
    public class GetQrCodeResponse : SuccessResponse
    {
        [HmacOrder(1)]
        public string Id { get; set; }

        [HmacOrder(2)]
        public string Url { get; set; }

        [HmacOrder(3)]
        public string Description { get; set; }
    }
}
