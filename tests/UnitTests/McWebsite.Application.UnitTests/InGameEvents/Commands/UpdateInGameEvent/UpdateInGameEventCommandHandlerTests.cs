using FluentAssertions;
using McWebsite.Domain.GameServer;
using Moq;
using Xunit;
using McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.InGameEventExtensions;
using McWebsite.Application.UnitTests.InGameEvents.TestUtils;
using McWebsite.Domain.InGameEvent.Entities;

namespace McWebsite.Application.UnitTests.InGameEvents.Commands.UpdateInGameEvent
{
    public sealed class UpdateInGameEventCommandHandlerTests
    {
        private readonly UpdateInGameEventCommandHandler _handler;
        private readonly UnitTestEnvironments.InGameEventTestEnvironment _inGameEventTestEnvironment;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _gameServerTestEnvironment;
        public UpdateInGameEventCommandHandlerTests()
        {
            _inGameEventTestEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();
            _gameServerTestEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();

            _handler = new UpdateInGameEventCommandHandler(_inGameEventTestEnvironment.MockInGameEventRepository.Object,
                _gameServerTestEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidUpdateInGameEventCommands))]
        public async Task HandleUpdateInGameEventCommand_ValidCommandGiven_ShouldReturnAndUpdateInGameEvent(UpdateInGameEventCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeFalse();
            var inGameEvent = commandResult.Value.Value.InGameEvent; // Nullable object (ErrorOr<UpdateInGameEventResult>?)
            inGameEvent.Should().NotBeNull();
            inGameEvent.ValidateIfUpdatedFrom(command);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.UpdateInGameEvent(It.IsAny<InGameEvent>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ValidButWithNoChangesUpdateInGameEventCommands))]
        public async Task HandleUpdateInGameEventCommand_ValidCommandGivenButNoChangeIsNeeded_ShouldReturnNull(UpdateInGameEventCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Should().BeNull();
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.UpdateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(NotExistingIdUpdateInGameEventCommands))]
        public async Task HandleUpdateInGameEventCommand_NotExistingIdCommandGiven_ShouldBeCatchedByValidator(UpdateInGameEventCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeTrue();
            commandResult.Value.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.UpdateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidInGameIdUpdateInGameEventCommands))]
        public async Task HandleUpdateInGameEventCommand_InvalidInGameIdCommandGiven_ShouldBeCatchedByValidator(UpdateInGameEventCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.UpdateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidInGameEventTypeUpdateInGameEventCommands))]
        public async Task HandleUpdateInGameEventCommand_InvalidInGameEventTypeCommandGiven_ShouldBeCatchedByValidator(UpdateInGameEventCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.UpdateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEmptyDescriptionUpdateInGameEventCommands))]
        public async Task HandleUpdateInGameEventCommand_InvalidEmptyDescriptionCommandGiven_ShouldBeCatchedByValidator(UpdateInGameEventCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.UpdateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidPriceUpdateInGameEventCommands))]
        public async Task HandleUpdateInGameEventCommand_InvalidPriceCommandGiven_ShouldBeCatchedByValidator(UpdateInGameEventCommand command)
        {
            // Arrange
            var validator = new UpdateInGameEventCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.UpdateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }
        public static IEnumerable<object[]> ValidUpdateInGameEventCommands()
        {
            var testInGameEventEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();

            yield return new[] { UpdateInGameEventCommandUtils.Create(testInGameEventEnvironment.InGameEvents[0].Id.Value,
                                                                      testInGameEventEnvironment.InGameEvents[0].GameServerId.Value,
                                                                      10005,
                                                                      "PlayerEvent",
                                                                      "New Description 1",
                                                                      3000) };

            yield return new[] { UpdateInGameEventCommandUtils.Create(testInGameEventEnvironment.InGameEvents[1].Id.Value,
                                                                      testInGameEventEnvironment.InGameEvents[1].GameServerId.Value,
                                                                      10006,
                                                                      "PlayerEvent",
                                                                      "New Description 2",
                                                                      3500) };
        }

        public static IEnumerable<object[]> ValidButWithNoChangesUpdateInGameEventCommands()
        {
            var testInGameEventEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();

            foreach(InGameEvent ige in testInGameEventEnvironment.InGameEvents)
            {
                yield return new[] { UpdateInGameEventCommandUtils.Create(ige.Id.Value,
                                                                      ige.GameServerId.Value,
                                                                      ige.InGameId,
                                                                      ige.InGameEventType.Value.ToString(),
                                                                      ige.Description,
                                                                      ige.Price) };
            }
        }
        public static IEnumerable<object[]> NotExistingIdUpdateInGameEventCommands()
        {
            yield return new[] { UpdateInGameEventCommandUtils.Create(Guid.NewGuid()) };

            yield return new[] { UpdateInGameEventCommandUtils.Create(Guid.NewGuid()) };
        }
        public static IEnumerable<object[]> InvalidInGameIdUpdateInGameEventCommands()
        {
            yield return new[] { UpdateInGameEventCommandUtils.Create(inGameId: 0) };

            yield return new[] { UpdateInGameEventCommandUtils.Create(inGameId: -50) };
        }
        public static IEnumerable<object[]> InvalidInGameEventTypeUpdateInGameEventCommands()
        {
            yield return new[] { UpdateInGameEventCommandUtils.Create(inGameEventType: "FreeMoney") };
            yield return new[] { UpdateInGameEventCommandUtils.Create(inGameEventType: "FreeAdminRoles") };
        }

        public static IEnumerable<object[]> InvalidEmptyDescriptionUpdateInGameEventCommands()
        {
            yield return new[] { UpdateInGameEventCommandUtils.Create(description: "") };
            yield return new[] { UpdateInGameEventCommandUtils.Create(description: "") };
        }
        public static IEnumerable<object[]> InvalidPriceUpdateInGameEventCommands()
        {
            yield return new[] { UpdateInGameEventCommandUtils.Create(price: 0) };
            yield return new[] { UpdateInGameEventCommandUtils.Create(price: -3000) };
        }
    }
}
