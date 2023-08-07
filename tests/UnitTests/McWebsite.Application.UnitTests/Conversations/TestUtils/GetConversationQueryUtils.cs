using McWebsite.Application.Conversations.Queries.GetConversationQuery;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Application.UnitTests.Conversations.TestUtils;

namespace McWebsite.Application.UnitTests.Conversations.TestUtils
{
    public static class GetConversationQueryUtils
    {
        public static GetConversationQuery Create(Guid? Id = null)
        {
            return new GetConversationQuery(Id ?? Constants.ConversationQueriesAndCommands.Id);
        }
    }
}
