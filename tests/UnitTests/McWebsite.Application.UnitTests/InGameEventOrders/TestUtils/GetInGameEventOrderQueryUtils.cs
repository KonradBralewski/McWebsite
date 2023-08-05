using McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrderQuery;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.InGameEventOrders.TestUtils
{
    public static class GetInGameEventOrderQueryUtils
    {
        public static GetInGameEventOrderQuery Create(Guid? Id = null)
        {
            return new GetInGameEventOrderQuery(Id ?? Constants.InGameEventOrderQueriesAndCommands.Id);
        }
    }
}
