using McWebsite.Application.GameServerReports.Commands.DeleteGameServerReportCommand;
using McWebsite.Application.GameServers.Commands.DeleteGameServerCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServersReports.TestUtils
{
    public static class DeleteGameServerReportCommandUtils
    {
        public static DeleteGameServerReportCommand Create(Guid? Id = null)
        {
            return new DeleteGameServerReportCommand(Id ?? Constants.GameServerReportQueriesAndCommands.Id);
        }
    }
}
