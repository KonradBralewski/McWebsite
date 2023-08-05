using McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrdersQuery;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.InGameEventOrders.TestUtils
{
    public static class GetInGameEventOrdersQueryUtils
    {
        public static GetInGameEventOrdersQuery Create(int? page = null, int? entriesPerPage = null)
        {
            return new GetInGameEventOrdersQuery(page ?? Constants.InGameEventOrderQueriesAndCommands.Page,
                                           entriesPerPage ?? Constants.InGameEventOrderQueriesAndCommands.EntriesPerPage);
        }
    }
}
