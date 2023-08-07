using McWebsite.Application.Messages.Commands.UpdateMessageCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Application.UnitTests.Messages.TestUtils;

namespace McWebsite.Application.UnitTests.Messages.TestUtils
{
    public static class UpdateMessageCommandUtils
    {
        public static UpdateMessageCommand Create(Guid? messageId = null,
                                                           string? messageContent = null)
        {
            return new UpdateMessageCommand(messageId ?? Constants.MessageQueriesAndCommands.Id,
                                            messageContent ?? Constants.MessageQueriesAndCommands.MessageContent);
        }
    }
}
