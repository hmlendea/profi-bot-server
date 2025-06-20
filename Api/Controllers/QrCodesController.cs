using System;

using Microsoft.AspNetCore.Mvc;
using ProfiBotServer.Api.Requests;

using NuciAPI.Responses;
using ProfiBotServer.Service;
using ProfiBotServer.Api.Responses;
using ProfiBotServer.Service.Models;

namespace ProfiBotServer.Api.Controllers
{
    [Route("QR")]
    [ApiController]
    public class QrCodesController(IQrCodeService service) : ControllerBase
    {
        [HttpGet]
        public ActionResult RecordPrize([FromBody] GetQrCodeRequest request)
        {
            if (request is null)
            {
                return BadRequest(new ErrorResponse(ErrorResponseMessages.InvalidRequest));
            }

            try
            {
                GetQrCodeResponse response = service.GetRandom(request);

                if (response is null)
                {
                    return NotFound(new ErrorResponse("No QR code was found."));
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }
    }
}
