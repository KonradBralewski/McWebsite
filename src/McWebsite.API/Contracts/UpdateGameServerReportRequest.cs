namespace McWebsite.API.Contracts
{
    public sealed record UpdateGameServerReportRequest(Guid ReportedGameServerId,
        string ReportType,
        string ReportDescription);
}
