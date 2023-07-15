using McWebsite.Application.GameServerReports.Queries.GetGameServersReportsQuery;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServersReports.TestUtils
{
    public static class GetGameServersReportsQueryUtils
    {
        public static GetGameServersReportsQuery Create(int? page = null, int? entriesPerPage = null)
        {
            return new GetGameServersReportsQuery(page ?? Constants.GameServerReportQueriesAndCommands.Page,
                                           entriesPerPage ?? Constants.GameServerReportQueriesAndCommands.EntriesPerPage);
        }
    }
}
