using FluentAssertions;
using McWebsite.Application.InGameEvents.Commands.DeleteInGameEventCommand;
using McWebsite.Application.UnitTests.InGameEvents.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.InGameEvent.Entities;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.InGameEvents.Commands.DeleteInGameEvent
{
    public sealed class DeleteInGameEventCommandHandlerTests
    {
        private readonly DeleteInGameEventCommandHandler _handler;
        private readonly UnitTestEnvironments.InGameEventTestEnvironment _testEnvironment;
        public DeleteInGameEventCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();
            _handler = new DeleteInGameEventCommandHandler(_testEnvironment.MockInGameEventRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdDeleteInGameEventCommands))]
        public async Task HandleDeleteInGameEventCommand_ValidIdCommandGiven_ShouldDeleteInGameEventAndReturnTrue(DeleteInGameEventCommand command)
        {
            // Arrange
            var validator = new DeleteInGameEventCommandValidator();
            int existingInGameEventsCount = _testEnvironment.InGameEvents.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.Should().BeTrue();
            commandResult.IsError.Should().BeFalse();
            _testEnvironment.MockInGameEventRepository.Verify(x => x.DeleteInGameEvent(It.IsAny<InGameEvent>()), Times.Once);
            _testEnvironment.InGameEvents.Count().Should().Be(existingInGameEventsCount-1);
        }

        [Theory]
        [MemberData(nameof(InvalidIdDeleteInGameEventCommands))]
        public async Task HandleDeleteInGameEventCommand_InvalidIdCommandGiven_ShouldReturnNotFoundError(DeleteInGameEventCommand command)
        {
            // Arrange
            var validator = new DeleteInGameEventCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockInGameEventRepository.Verify(x => x.DeleteInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }
       
        public static IEnumerable<object[]> ValidIdDeleteInGameEventCommands()
        {
            var testEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();
            foreach (InGameEvent ige in testEnvironment.InGameEvents)
            {
                yield return new[] { DeleteInGameEventCommandUtils.Create(ige.Id.Value) };
            }
        }
        public static IEnumerable<object[]> InvalidIdDeleteInGameEventCommands()
        {
            yield return new[] { DeleteInGameEventCommandUtils.Create(Guid.NewGuid()) };
            yield return new[] { DeleteInGameEventCommandUtils.Create(Guid.NewGuid()) };
        }
    }
}
