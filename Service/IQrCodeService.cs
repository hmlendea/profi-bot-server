using ProfiBotServer.Api.Requests;
using ProfiBotServer.Service.Models;

namespace ProfiBotServer.Service
{
    public interface IQrCodeService
    {
        QrCode GetRandom(GetQrCodeRequest request);
    }
}
