using FluentAssertions;
using McWebsite.Application.Conversations.Commands.CreateConversationCommand;
using McWebsite.Domain.Conversation;

namespace McWebsite.Application.UnitTests.TestUtils.Extensions.ConversationExtensions
{
    public static class ConversationValidationExtensions
    {
        public static void ValidateIfCreatedFrom(this Conversation conversation, CreateConversationCommand command)
        {
            conversation.Id.Value.ToString().Should().NotBeEmpty();

            conversation.Participants.FirstParticipantId.Should().Be(command.FirstParticipantId);

            conversation.Participants.SecondParticipantId.Should().Be(command.SecondParticipantId);
        }
    }
}
