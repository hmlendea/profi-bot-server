using System;

using Microsoft.AspNetCore.Mvc;
using ProfiBotServer.Api.Requests;

using NuciAPI.Responses;

namespace ProfiBotServer.Api.Controllers
{
    [Route("QR")]
    [ApiController]
    public class QrCodesController() : ControllerBase
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
                return Ok(SuccessResponse.Default);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
        }
    }
}
