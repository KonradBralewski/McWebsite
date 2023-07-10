namespace McWebsite.API.Contracts.GameServerReport
{
    public sealed record CreateGameServerReportRequest(Guid ReportedGameServerId,
                                                       string ReportType,
                                                       string ReportDescription);
}
