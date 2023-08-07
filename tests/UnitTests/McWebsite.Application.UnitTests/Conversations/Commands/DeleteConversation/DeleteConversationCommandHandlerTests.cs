using FluentAssertions;
using McWebsite.Application.Conversations.Commands.DeleteConversationCommand;
using McWebsite.Application.Conversations.Commands.DeleteConversationCommand;
using McWebsite.Application.UnitTests.Conversations.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.Conversation;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.Conversations.Commands.DeleteConversation
{
    public sealed class DeleteConversationCommandHandlerTests
    {
        private readonly DeleteConversationCommandHandler _handler;
        private readonly UnitTestEnvironments.ConversationTestEnvironment _testEnvironment;
        public DeleteConversationCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.ConversationTestEnvironment.Create();
            _handler = new DeleteConversationCommandHandler(_testEnvironment.MockConversationRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdDeleteConversationCommands))]
        public async Task HandleDeleteConversationCommand_ValidIdCommandGiven_ShouldDeleteConversationAndReturnTrue(DeleteConversationCommand command)
        {
            // Arrange
            var validator = new DeleteConversationCommandValidator();
            int existingConversationsCount = _testEnvironment.Conversations.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.Should().BeTrue();
            commandResult.IsError.Should().BeFalse();
            _testEnvironment.MockConversationRepository.Verify(x => x.DeleteConversation(It.IsAny<Conversation>()), Times.Once);
            _testEnvironment.Conversations.Count().Should().Be(existingConversationsCount-1);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingIdDeleteConversationCommands))]
        public async Task HandleDeleteConversationCommand_InvalidNotExistingIdCommandGiven_ShouldReturnNotFoundError(DeleteConversationCommand command)
        {
            // Arrange
            var validator = new DeleteConversationCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockConversationRepository.Verify(x => x.DeleteConversation(It.IsAny<Conversation>()), Times.Never);
        }
       
        public static IEnumerable<object[]> ValidIdDeleteConversationCommands()
        {
            var testEnvironment = UnitTestEnvironments.ConversationTestEnvironment.Create();

            foreach (Conversation gs in testEnvironment.Conversations)
            {
                yield return new[] { DeleteConversationCommandUtils.Create(gs.Id.Value) };
            }
        }
        public static IEnumerable<object[]> InvalidNotExistingIdDeleteConversationCommands()
        {
            yield return new[] { DeleteConversationCommandUtils.Create(Guid.NewGuid()) };
            yield return new[] { DeleteConversationCommandUtils.Create(Guid.NewGuid()) };
        }
    }
}
