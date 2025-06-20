using ProfiBotServer.Api.Requests;
using ProfiBotServer.Api.Responses;

namespace ProfiBotServer.Service
{
    public interface IQrCodeService
    {
        GetQrCodeResponse GetRandom(GetQrCodeRequest request);
    }
}
