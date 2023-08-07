using McWebsite.Application.Conversations.Queries.GetConversationsQuery;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.Conversations.TestUtils
{
    public static class GetConversationsQueryUtils
    {
        public static GetConversationsQuery Create(int? page = null, int? entriesPerPage = null)
        {
            return new GetConversationsQuery(page ?? Constants.ConversationQueriesAndCommands.Page,
                                           entriesPerPage ?? Constants.ConversationQueriesAndCommands.EntriesPerPage);
        }
    }
}
