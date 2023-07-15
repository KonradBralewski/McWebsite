using McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServersReports.TestUtils
{
    public static class UpdateGameServerReportCommandUtils
    {
        public static UpdateGameServerReportCommand Create(Guid? id = null,
                                                     Guid? reportedGameServerId = null,
                                                     string? reportType = null,
                                                     string? description = null)
        {
            return new UpdateGameServerReportCommand(id ?? Constants.GameServerReportQueriesAndCommands.Id,
                                               reportedGameServerId ?? Constants.GameServerReportQueriesAndCommands.ReportedGameServerId,
                                               reportType ?? Constants.GameServerReportQueriesAndCommands.ReportType,
                                               description ?? Constants.GameServerReportQueriesAndCommands.Description);
        }
    }
}
