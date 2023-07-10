using ErrorOr;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.ValueObjects;

namespace McWebsite.Application.Common.Interfaces.Persistence
{
    public interface IGameServerReportRepository
    {
        Task<ErrorOr<IEnumerable<GameServerReport>>> GetGameServersReports(int page, int entriesPerPage);
        Task<ErrorOr<GameServerReport>> GetGameServerReport(GameServerReportId gameServerReportId);
        Task<ErrorOr<GameServerReport>> CreateGameServerReport(GameServerReport gameServerReport);
        Task<ErrorOr<GameServerReport>> UpdateGameServerReport(GameServerReport gameServerReport);
        Task DeleteGameServerReport(GameServerReport gameServerReport);
    }
}
