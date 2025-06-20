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
                QrCode qrCode = service.GetRandom(request);

                if (qrCode is null)
                {
                    return NotFound(new ErrorResponse("No QR code was found."));
                }

                GetQrCodeResponse response = new()
                {
                    Id = qrCode.Id
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }
    }
}
