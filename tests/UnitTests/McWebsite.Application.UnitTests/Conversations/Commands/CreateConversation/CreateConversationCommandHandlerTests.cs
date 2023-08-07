using FluentAssertions;
using McWebsite.Application.Conversations.Commands.CreateConversationCommand;
using McWebsite.Application.UnitTests.Messages.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.ConversationExtensions;
using McWebsite.Domain.Conversation;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.Conversations.Commands.CreateConversation
{
    public sealed class CreateConversationCommandHandlerTests
    {
        private readonly CreateConversationCommandHandler _handler;
        private readonly UnitTestEnvironments.ConversationTestEnvironment _conversationTestEnvironment;
        private readonly UnitTestEnvironments.UserTestEnvironment _userTestEnvironment;
        public CreateConversationCommandHandlerTests()
        {
            _conversationTestEnvironment = UnitTestEnvironments.ConversationTestEnvironment.Create();
            _userTestEnvironment = UnitTestEnvironments.UserTestEnvironment.Create();

            _handler = new CreateConversationCommandHandler(_conversationTestEnvironment.MockConversationRepository.Object,
                                                            _userTestEnvironment.MockUserRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidCreateConversationCommands))]
        public async Task HandleCreateConversationCommand_ValidCommandGiven_ShouldReturnAndCreateConversation(CreateConversationCommand command)
        {
            // Arrange
            var validator = new CreateConversationCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.Conversation.Should().NotBeNull();
            commandResult.IsError.Should().BeFalse();
            commandResult.Value.Conversation.ValidateIfCreatedFrom(command);
            _conversationTestEnvironment.MockConversationRepository.Verify(x => x.CreateConversation(It.IsAny<Conversation>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingUserIdCreateConversationCommands))]
        public async Task HandleCreateConversationCommand_InvalidNotExistingUserIdCommandGiven_ShouldReturnNotFoundError(CreateConversationCommand command)
        {
            // Arrange
            var validator = new CreateConversationCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _conversationTestEnvironment.MockConversationRepository.Verify(x => x.CreateConversation(It.IsAny<Conversation>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidCreateConversationCommands()
        {
            var gameEventTestEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();

            yield return new[] { CreateMessageCommandUtils.Create() };
            yield return new[] { CreateMessageCommandUtils.Create(messageContent: "HI") };
            yield return new[] { CreateMessageCommandUtils.Create(messageContent: "HELLO!") };
        }

        public static IEnumerable<object[]> InvalidNotExistingUserIdCreateConversationCommands()
        {
            yield return new[] { CreateMessageCommandUtils.Create(receiverId: Guid.NewGuid()) };
            yield return new[] { CreateMessageCommandUtils.Create(receiverId: Guid.NewGuid()) };
            yield return new[] { CreateMessageCommandUtils.Create(shipperId: Guid.NewGuid()) };
            yield return new[] { CreateMessageCommandUtils.Create(shipperId: Guid.NewGuid()) };
            yield return new[] { CreateMessageCommandUtils.Create(Guid.NewGuid(), Guid.NewGuid()) };

        }
    }
}
