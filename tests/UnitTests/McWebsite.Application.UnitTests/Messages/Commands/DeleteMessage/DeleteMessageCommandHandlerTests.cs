using FluentAssertions;
using McWebsite.Application.Messages.Commands.DeleteMessageCommand;
using McWebsite.Application.UnitTests.Messages.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.Message.Entities;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.Messages.Commands.DeleteMessage
{
    public sealed class DeleteMessageCommandHandlerTests
    {
        private readonly DeleteMessageCommandHandler _handler;
        private readonly UnitTestEnvironments.MessageTestEnvironment _testEnvironment;
        public DeleteMessageCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();
            _handler = new DeleteMessageCommandHandler(_testEnvironment.MockMessageRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdDeleteMessageCommands))]
        public async Task HandleDeleteMessageCommand_ValidIdCommandGiven_ShouldDeleteMessageAndReturnTrue(DeleteMessageCommand command)
        {
            // Arrange
            var validator = new DeleteMessageCommandValidator();
            int existingMessagesCount = _testEnvironment.Messages.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.Should().BeTrue();
            commandResult.IsError.Should().BeFalse();
            _testEnvironment.MockMessageRepository.Verify(x => x.DeleteMessage(It.IsAny<Message>()), Times.Once);
            _testEnvironment.Messages.Count().Should().Be(existingMessagesCount-1);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingIdDeleteMessageCommands))]
        public async Task HandleDeleteMessageCommand_InvalidNotExistingIdCommandGiven_ShouldReturnNotFoundError(DeleteMessageCommand command)
        {
            // Arrange
            var validator = new DeleteMessageCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockMessageRepository.Verify(x => x.DeleteMessage(It.IsAny<Message>()), Times.Never);
        }
       
        public static IEnumerable<object[]> ValidIdDeleteMessageCommands()
        {
            var testEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();

            foreach (Message msg in testEnvironment.Messages)
            {
                yield return new[] { DeleteMessageCommandUtils.Create(msg.Id.Value) };
            }
        }
        public static IEnumerable<object[]> InvalidNotExistingIdDeleteMessageCommands()
        {
            yield return new[] { DeleteMessageCommandUtils.Create(Guid.NewGuid()) };
            yield return new[] { DeleteMessageCommandUtils.Create(Guid.NewGuid()) };
        }
    }
}
