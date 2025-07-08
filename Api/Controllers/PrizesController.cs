using Microsoft.AspNetCore.Mvc;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.Service;
using NuciAPI.Controllers;

namespace ProfiBotServer.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrizesController(IPrizeService service) : NuciApiController
    {
        [HttpPost]
        public ActionResult RecordPrize([FromBody] RecordPrizeRequest request)
            => ProcessRequest(request, () => service.RecordPrize(request));
    }
}
