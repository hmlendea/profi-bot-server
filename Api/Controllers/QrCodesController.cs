using Microsoft.AspNetCore.Mvc;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.Service;
using NuciAPI.Controllers;

namespace ProfiBotServer.Api.Controllers
{
    [Route("QR")]
    [ApiController]
    public class QrCodesController(IQrCodeService service) : NuciApiController
    {
        [HttpPost]
        public ActionResult Add([FromBody] AddQrCodeRequest request)
            => ProcessRequest(request, () => service.Add(request));

        [HttpGet]
        public ActionResult GetRandom([FromBody] GetQrCodeRequest request)
            => ProcessRequest(request, () => service.GetRandomEnabled(request));
    }
}
