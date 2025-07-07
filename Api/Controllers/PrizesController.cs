using System;

using Microsoft.AspNetCore.Mvc;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.Service;

using NuciAPI.Responses;
using System.Security;

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
                return BadRequest(new ErrorResponse(ErrorResponseMessages.InvalidRequest));
            }

            try
            {
                service.RecordPrize(request);

                return Ok(SuccessResponse.Default);
            }
            catch (SecurityException ex)
            {
                return Unauthorized(new ErrorResponse(ex));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }
    }
}
