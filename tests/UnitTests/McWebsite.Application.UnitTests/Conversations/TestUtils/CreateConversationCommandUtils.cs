using McWebsite.Application.Conversations.Commands.CreateConversationCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.Conversations.TestUtils
{
    public static class CreateConversationCommandUtils
    {
        public static CreateConversationCommand Create(Guid? firstParticipantId = null,
                                                       Guid? secondParticipantId = null,
                                                       string? firstMessageContent = null)
        {
            return new CreateConversationCommand(firstParticipantId ?? Constants.ConversationQueriesAndCommands.FirstParticipant,
                                                     secondParticipantId ?? Constants.ConversationQueriesAndCommands.SecondParticipant,
                                                     firstMessageContent ?? Constants.ConversationQueriesAndCommands.FirstMessageContent);
        }
    }
}
