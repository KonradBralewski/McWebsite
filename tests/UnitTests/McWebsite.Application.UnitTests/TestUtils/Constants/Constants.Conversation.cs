
namespace McWebsite.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class ConversationQueriesAndCommands
        {
            public static string FirstMessageContent = "Hi!";
            public static List<Guid> MessagesIds = new();

            public static int Page = 0;
            public static int EntriesPerPage = 50;

            public static Guid Id = Guid.NewGuid();
            public static Guid FirstParticipant = MessageQueriesAndCommands.ReceiverId;
            public static Guid SecondParticipant = MessageQueriesAndCommands.ShipperId;
        }


    }
}
