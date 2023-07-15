using McWebsite.Application.GameServerReports.Queries.GetGameServerReportQuery;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServersReports.TestUtils
{
    public static class GetGameServerReportQueryUtils
    {
        public static GetGameServerReportQuery Create(Guid? Id = null)
        {
            return new GetGameServerReportQuery(Id ?? Constants.GameServerReportQueriesAndCommands.Id);
        }
    }
}
