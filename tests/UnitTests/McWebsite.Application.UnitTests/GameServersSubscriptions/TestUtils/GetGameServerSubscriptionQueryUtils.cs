using McWebsite.Application.GameServerSubscriptions.Queries.GetGameServerSubscriptionQuery;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServersSubscriptions.TestUtils
{
    public static class GetGameServerSubscriptionQueryUtils
    {
        public static GetGameServerSubscriptionQuery Create(Guid? Id = null)
        {
            return new GetGameServerSubscriptionQuery(Id ?? Constants.GameServerSubscriptionQueriesAndCommands.Id);
        }
    }
}
