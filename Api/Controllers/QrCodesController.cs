using System;

using Microsoft.AspNetCore.Mvc;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.Service;
using ProfiBotServer.Api.Responses;
using System.Security;
using NuciAPI.Responses;

namespace ProfiBotServer.Api.Controllers
{
    [Route("QR")]
    [ApiController]
    public class QrCodesController(IQrCodeService service) : ControllerBase
    {
        [HttpPost]
        public ActionResult Add([FromBody] AddQrCodeRequest request)
        {
            if (request is null)
            {
                return BadRequest(NuciApiErrorResponse.InvalidRequest);
            }

            try
            {
                service.Add(request);

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

        [HttpGet]
        public ActionResult GetRandom([FromBody] GetQrCodeRequest request)
        {
            if (request is null)
            {
                return BadRequest(NuciApiErrorResponse.InvalidRequest);
            }

            try
            {
                GetQrCodeResponse response = service.GetRandomEnabled(request);

                if (response is null)
                {
                    return NotFound(new NuciApiErrorResponse("No QR code was found."));
                }

                return Ok(response);
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
