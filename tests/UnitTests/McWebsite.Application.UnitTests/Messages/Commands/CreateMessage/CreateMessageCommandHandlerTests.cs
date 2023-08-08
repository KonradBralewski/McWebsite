using FluentAssertions;
using McWebsite.Application.Messages.Commands.CreateMessageCommand;
using McWebsite.Application.UnitTests.Messages.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.MessageExtensions;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.User.ValueObjects;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.Messages.Commands.CreateMessage
{
    public sealed class CreateMessageCommandHandlerTests
    {
        private readonly CreateMessageCommandHandler _handler;
        private readonly UnitTestEnvironments.MessageTestEnvironment _messageTestEnvironment;
        private readonly UnitTestEnvironments.ConversationTestEnvironment _conversationTestEnvironment;
        private readonly UnitTestEnvironments.UserTestEnvironment _userTestEnvironment;
        public CreateMessageCommandHandlerTests()
        {
            _messageTestEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();
            _conversationTestEnvironment = UnitTestEnvironments.ConversationTestEnvironment.Create();
            _userTestEnvironment = UnitTestEnvironments.UserTestEnvironment.Create();

            _handler = new CreateMessageCommandHandler(_messageTestEnvironment.MockMessageRepository.Object,
                                                       _conversationTestEnvironment.MockConversationRepository.Object,
                                                       _userTestEnvironment.MockUserRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidCreateMessageCommands))]
        public async Task HandleCreateMessageCommand_ValidCommandGiven_ShouldReturnAndCreateMessage(CreateMessageCommand command)
        {
            // Arrange
            var validator = new CreateMessageCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.Message.Should().NotBeNull();
            commandResult.IsError.Should().BeFalse();
            commandResult.Value.Message.ValidateIfCreatedFrom(command);
            _messageTestEnvironment.MockMessageRepository.Verify(x => x.CreateMessage(It.IsAny<Message>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingUserIdCreateMessageCommands))]
        public async Task HandleCreateMessageCommand_InvalidNotExistingUserIdCommandGiven_ShouldReturnNotFoundError(CreateMessageCommand command)
        {
            // Arrange
            var validator = new CreateMessageCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _messageTestEnvironment.MockMessageRepository.Verify(x => x.CreateMessage(It.IsAny<Message>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidCreateMessageCommands()
        {
            yield return new[] { CreateMessageCommandUtils.Create() };

            yield return new[] { CreateMessageCommandUtils.Create(messageContent: "Hello!") };

            yield return new[] { CreateMessageCommandUtils.Create(messageContent: "Hello :)") };
        }

        public static IEnumerable<object[]> InvalidNotExistingUserIdCreateMessageCommands()
        {
            yield return new[] { CreateMessageCommandUtils.Create(Guid.NewGuid(), Guid.NewGuid()) };
            yield return new[] { CreateMessageCommandUtils.Create(receiverId: Guid.NewGuid()) };
            yield return new[] { CreateMessageCommandUtils.Create(receiverId: Guid.NewGuid()) };
            yield return new[] { CreateMessageCommandUtils.Create(shipperId: Guid.NewGuid()) };
            yield return new[] { CreateMessageCommandUtils.Create(shipperId: Guid.NewGuid()) };
        }
    }
}
