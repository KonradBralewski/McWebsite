using McWebsite.Application.GameServerSubscriptions.Queries.GetGameServersSubscriptionsQuery;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServersSubscriptions.TestUtils
{
    public static class GetGameServersSubscriptionsQueryUtils
    {
        public static GetGameServersSubscriptionsQuery Create(int? page = null, int? entriesPerPage = null)
        {
            return new GetGameServersSubscriptionsQuery(page ?? Constants.GameServerSubscriptionQueriesAndCommands.Page,
                                           entriesPerPage ?? Constants.GameServerSubscriptionQueriesAndCommands.EntriesPerPage);
        }
    }
}
