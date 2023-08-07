using FluentAssertions;
using McWebsite.Application.Messages.Commands.CreateMessageCommand;
using McWebsite.Application.Messages.Commands.UpdateMessageCommand;
using McWebsite.Domain.Message.Entities;

namespace McWebsite.Application.UnitTests.TestUtils.Extensions.MessageExtensions
{
    public static class MessageValidationExtensions
    {
        public static void ValidateIfCreatedFrom(this Message message, CreateMessageCommand command)
        {
            message.Id.Value.ToString().Should().NotBeEmpty();

            message.ConversationId.ToString().Should().NotBeEmpty();

            message.MessageContent.Should().Be(command.MessageContent);

            message.ReceiverId.Value.Should().Be(command.ReceiverId);

            message.ShipperId.Value.Should().Be(command.ShipperId);
        }

        public static void ValidateIfUpdatedFrom(this Message message, UpdateMessageCommand command)
        {
            message.Id.Value.Should().Be(command.MessageId);

            message.MessageContent.Should().Be(command.MessageContent);
        }
    }
}
