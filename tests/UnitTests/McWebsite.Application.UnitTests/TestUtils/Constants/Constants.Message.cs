namespace McWebsite.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class MessageQueriesAndCommands
        {
            public static int Page = 0;
            public static int EntriesPerPage = 50;

            public static Guid Id = Guid.NewGuid();
            public static Guid ReceiverId = UserQueriesAndCommands.Id;
            public static Guid ShipperId = Guid.NewGuid();
            public static string MessageContent = "Hey! This my new message!";
        }
    }
}
