namespace McWebsite.API.Contracts.GameServerReport
{
    public sealed record GetGameServerReportResponse(Guid Id,
                                                     Guid ReportedGameServerId,
                                                     string ReportType,
                                                     string ReportDescription,
                                                     DateTime ReportDate);
}
