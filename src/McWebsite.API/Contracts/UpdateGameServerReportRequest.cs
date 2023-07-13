namespace McWebsite.API.Contracts
{
    public sealed record UpdateGameServerReportRequest(Guid GameServerId,
        string ReportType,
        string ReportDescription);
}
