using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.ValueObjects;

namespace McWebsite.Application.Common.Interfaces.Persistence
{
    public interface IGameServerReportRepository<TId>
    {
        Task<IEnumerable<GameServerReport>> GetGameServersReports(int page, int entriesPerPage);
        Task<GameServerReport> GetGameServerReport(TId id);
        Task<GameServerReport> CreateGameServerReport();
        Task<GameServerReport> UpdateGameServerReport();
        Task DeleteGameServerReport();
    }
}
