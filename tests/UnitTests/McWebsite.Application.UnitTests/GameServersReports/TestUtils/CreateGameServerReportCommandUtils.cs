using McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServersReports.TestUtils
{
    public static class CreateGameServerReportCommandUtils
    {
        public static CreateGameServerReportCommand Create(Guid? reportingUserId = null,
                                                     Guid? reportedGameServerId = null,
                                                     string? reportType = null,
                                                     string? description = null)
        {
            return new CreateGameServerReportCommand(reportingUserId ?? Constants.GameServerReportQueriesAndCommands.ReportingUserId,
                                               reportedGameServerId ?? Constants.GameServerReportQueriesAndCommands.ReportedGameServerId,
                                               reportType ?? Constants.GameServerReportQueriesAndCommands.ReportType,
                                               description ?? Constants.GameServerReportQueriesAndCommands.Description);
        }
    }
}
