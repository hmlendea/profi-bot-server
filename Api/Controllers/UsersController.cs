using Microsoft.AspNetCore.Mvc;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.Service;
using NuciAPI.Controllers;

namespace ProfiBotServer.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(IUserService service) : NuciApiController
    {
        [HttpGet("balance")]
        public ActionResult GetBalance([FromBody] GetUserBalanceRequest request)
            => ProcessRequest(request, () => service.GetBalance(request));
    }
}
