using McWebsite.Application.Conversations.Commands.DeleteConversationCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.Conversations.TestUtils
{
    public static class DeleteConversationCommandUtils
    {
        public static DeleteConversationCommand Create(Guid? Id = null)
        {
            return new DeleteConversationCommand(Id ?? Constants.ConversationQueriesAndCommands.Id);
        }
    }
}
