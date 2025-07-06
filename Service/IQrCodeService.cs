using ProfiBotServer.Api.Requests;
using ProfiBotServer.Api.Responses;

namespace ProfiBotServer.Service
{
    public interface IQrCodeService
    {
        void Add(AddQrCodeRequest request);

        GetQrCodeResponse GetRandomEnabled(GetQrCodeRequest request);
    }
}
