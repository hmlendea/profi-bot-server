using System;

using Microsoft.AspNetCore.Mvc;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.Service;
using System.Security;
using NuciAPI.Responses;

namespace ProfiBotServer.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrizesController(IPrizeService service) : ControllerBase
    {
        [HttpPost]
        public ActionResult RecordPrize([FromBody] RecordPrizeRequest request)
        {
            if (request is null)
            {
                return BadRequest(NuciApiErrorResponse.InvalidRequest);
            }

            try
            {
                service.RecordPrize(request);

                return Ok(NuciApiSuccessResponse.Default);
            }
            catch (SecurityException ex)
            {
                return Unauthorized(new NuciApiErrorResponse(ex));
            }
            catch (Exception ex)
            {
                return BadRequest(new NuciApiErrorResponse(ex));
            }
        }
    }
}
