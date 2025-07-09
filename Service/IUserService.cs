using ProfiBotServer.Api.Requests;
using ProfiBotServer.Api.Responses;

namespace ProfiBotServer.Service
{
    public interface IUserService
    {
        GetUserBalanceResponse GetBalance(GetUserBalanceRequest request);
    }
}
