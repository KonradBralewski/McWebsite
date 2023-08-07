using McWebsite.Application.Messages.Queries.GetMessagesQuery;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.Messages.TestUtils
{
    public static class GetMessagesQueryUtils
    {
        public static GetMessagesQuery Create(int? page = null, int? entriesPerPage = null)
        {
            return new GetMessagesQuery(page ?? Constants.MessageQueriesAndCommands.Page,
                                           entriesPerPage ?? Constants.MessageQueriesAndCommands.EntriesPerPage);
        }
    }
}
