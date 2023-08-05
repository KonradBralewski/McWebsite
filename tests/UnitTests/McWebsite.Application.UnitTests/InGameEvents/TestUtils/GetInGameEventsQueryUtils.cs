using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventsQuery;

namespace McWebsite.Application.UnitTests.InGameEvents.TestUtils
{
    public static class GetInGameEventsQueryUtils
    {
        public static GetInGameEventsQuery Create(int? page = null, int? entriesPerPage = null)
        {
            return new GetInGameEventsQuery(page ?? Constants.InGameEventQueriesAndCommands.Page,
                                           entriesPerPage ?? Constants.InGameEventQueriesAndCommands.EntriesPerPage);
        }
    }
}
