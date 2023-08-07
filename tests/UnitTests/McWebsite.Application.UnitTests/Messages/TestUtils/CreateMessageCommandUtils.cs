using McWebsite.Application.Messages.Commands.CreateMessageCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Application.UnitTests.Messages.TestUtils;

namespace McWebsite.Application.UnitTests.Messages.TestUtils
{
    public static class CreateMessageCommandUtils
    {
        public static CreateMessageCommand Create(Guid? receiverId = null, Guid? shipperId = null,
                                                           string? messageContent = null)
        {
            return new CreateMessageCommand(receiverId ?? Constants.MessageQueriesAndCommands.ReceiverId,
                                            shipperId ?? Constants.MessageQueriesAndCommands.ShipperId,
                                            messageContent ?? Constants.MessageQueriesAndCommands.MessageContent);
        }
    }
}
