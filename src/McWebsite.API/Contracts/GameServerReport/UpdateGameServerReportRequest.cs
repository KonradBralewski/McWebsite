namespace McWebsite.API.Contracts.GameServerReport
{
    public sealed record UpdateGameServerReportRequest(Guid ReportedGameServerId,
        string ReportType,
        string ReportDescription);
}
