using McWebsite.Application.Messages.Queries.GetMessageQuery;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Application.UnitTests.Messages.TestUtils;

namespace McWebsite.Application.UnitTests.Messages.TestUtils
{
    public static class GetMessageQueryUtils
    {
        public static GetMessageQuery Create(Guid? Id = null)
        {
            return new GetMessageQuery(Id ?? Constants.MessageQueriesAndCommands.Id);
        }
    }
}
