using ProfiBotServer.Api.Requests;

namespace ProfiBotServer.Service
{
    public interface IPrizeService
    {
        void RecordPrize(RecordPrizeRequest request);
    }
}
