using FluentAssertions;
using McWebsite.Application.Messages.Commands.UpdateMessageCommand;
using McWebsite.Application.UnitTests.Messages.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.MessageExtensions;
using McWebsite.Domain.Message.Entities;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.Messages.Commands.UpdateMessage
{
    public sealed class UpdateMessageCommandHandlerTests
    {
        private readonly UpdateMessageCommandHandler _handler;
        private readonly UnitTestEnvironments.MessageTestEnvironment _testEnvironment;
        public UpdateMessageCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();

            _handler = new UpdateMessageCommandHandler(_testEnvironment.MockMessageRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidUpdateMessageCommands))]
        public async Task HandleUpdateMessageCommand_ValidCommandGiven_ShouldReturnAndUpdateMessage(UpdateMessageCommand command)
        {
            // Arrange
            var validator = new UpdateMessageCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeFalse();
            var message = commandResult.Value.Value.Message; // Nullable object (ErrorOr<UpdateMessageResult>?)
            message.Should().NotBeNull();
            message.ValidateIfUpdatedFrom(command);
            _testEnvironment.MockMessageRepository.Verify(x => x.UpdateMessage(It.IsAny<Message>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ValidButWithNoChangesUpdateMessageCommands))]
        public async Task HandleUpdateMessageCommand_ValidCommandGivenButNoChangeIsNeeded_ShouldReturnNull(UpdateMessageCommand command)
        {
            // Arrange
            var validator = new UpdateMessageCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Should().BeNull();
            _testEnvironment.MockMessageRepository.Verify(x => x.UpdateMessage(It.IsAny<Message>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(InvalidNotExistingIdUpdateMessageCommands))]
        public async Task HandleUpdateMessageCommand_InvalidNotExistingIdCommandGiven_ShouldReturnNotFoundError(UpdateMessageCommand command)
        {
            // Arrange
            var validator = new UpdateMessageCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeTrue();
            commandResult.Value.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockMessageRepository.Verify(x => x.UpdateMessage(It.IsAny<Message>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidMessageContentUpdateMessageCommands))]
        public async Task HandleUpdateMessageCommand_InvalidMessageContentCommandGiven_ShouldBeCatchedByValidator(UpdateMessageCommand command)
        {  // Arrange
            var validator = new UpdateMessageCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockMessageRepository.Verify(x => x.UpdateMessage(It.IsAny<Message>()), Times.Never);
        }
        public static IEnumerable<object[]> ValidUpdateMessageCommands()
        {
            var testEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();

            yield return new[] { UpdateMessageCommandUtils.Create(testEnvironment.Messages[0].Id.Value,
                                                                    "New message 1")};

            yield return new[] { UpdateMessageCommandUtils.Create(testEnvironment.Messages[1].Id.Value,
                                                                  "New message 2")};
        }

        public static IEnumerable<object[]> ValidButWithNoChangesUpdateMessageCommands()
        {
            var testEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();

            foreach(Message msg in testEnvironment.Messages)
            {
                yield return new[] { UpdateMessageCommandUtils.Create(msg.Id.Value,
                                                                      msg.MessageContent) };
            }
           
        }
        public static IEnumerable<object[]> InvalidNotExistingIdUpdateMessageCommands()
        {
            yield return new[] { UpdateMessageCommandUtils.Create(Guid.NewGuid()) };

            yield return new[] { UpdateMessageCommandUtils.Create(Guid.NewGuid()) };
        }
        public static IEnumerable<object[]> InvalidMessageContentUpdateMessageCommands()
        {
            yield return new[] { UpdateMessageCommandUtils.Create(messageContent: "") };

            yield return new[] { UpdateMessageCommandUtils.Create(messageContent: "") };
        }
    }
}
