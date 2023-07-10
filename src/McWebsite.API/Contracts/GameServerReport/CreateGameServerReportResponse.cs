namespace McWebsite.API.Contracts.GameServerReport
{
    public sealed record CreateGameServerReportResponse(Guid Id,
                                                        Guid ReportedGameServerId,
                                                        string ReportType,
                                                        string ReportDescription,
                                                        DateTime ReportDateTime);
}
