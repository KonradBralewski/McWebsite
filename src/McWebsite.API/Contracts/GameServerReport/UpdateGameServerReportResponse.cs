namespace McWebsite.API.Contracts.GameServerReport
{
    public sealed record UpdateGameServerReportResponse(Guid Id,
                                                        Guid ReportedGameServerId,
                                                        string ReportType,
                                                        string ReportDescription,
                                                        DateTime ReportDateTime);
}
