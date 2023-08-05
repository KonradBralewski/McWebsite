using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventQuery;

namespace McWebsite.Application.UnitTests.InGameEvents.TestUtils
{
    public static class GetInGameEventQueryUtils
    {
        public static GetInGameEventQuery Create(Guid? Id = null)
        {
            return new GetInGameEventQuery(Id ?? Constants.InGameEventQueriesAndCommands.Id);
        }
    }
}
