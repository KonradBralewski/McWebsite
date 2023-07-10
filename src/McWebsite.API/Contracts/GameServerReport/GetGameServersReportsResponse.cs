namespace McWebsite.API.Contracts.GameServerReport
{
    public sealed record GetGameServersReportsResponse(IEnumerable<GetGameServerReportResponse> GameServersReports);
}
