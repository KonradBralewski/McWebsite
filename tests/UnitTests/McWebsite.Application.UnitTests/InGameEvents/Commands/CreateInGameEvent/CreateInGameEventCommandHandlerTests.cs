using FluentAssertions;
using McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand;
using McWebsite.Application.InGameEvents.Commands.CreateInGameEventCommand;
using McWebsite.Application.UnitTests.InGameEvents.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.TestUtils.Extensions.InGameEventExtensions;
using McWebsite.Domain.InGameEvent.Entities;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.InGameEvents.Commands.CreateInGameEvent
{
    public sealed class CreateInGameEventCommandHandlerTests
    {
        private readonly CreateInGameEventCommandHandler _handler;
        private readonly UnitTestEnvironments.InGameEventTestEnvironment _inGameEventTestEnvironment;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _gameServerTestEnvironment;
        public CreateInGameEventCommandHandlerTests()
        {
            _inGameEventTestEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();
            _gameServerTestEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();

            _handler = new CreateInGameEventCommandHandler(_inGameEventTestEnvironment.MockInGameEventRepository.Object,
                _gameServerTestEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidCreateInGameEventCommands))]
        public async Task HandleCreateInGameEventCommand_ValidCommandGiven_ShouldReturnAndCreateInGameEvent(CreateInGameEventCommand command)
        {
            // Arrange
            var validator = new CreateInGameEventCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.InGameEvent.Should().NotBeNull();
            commandResult.IsError.Should().BeFalse();
            commandResult.Value.InGameEvent.ValidateIfCreatedFrom(command);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.CreateInGameEvent(It.IsAny<InGameEvent>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingGameServerIdCreateInGameEventCommands))]
        public async Task HandleCreateInGameEventCommand_InvalidNotExistingGameServerIdGiven_ShouldReturnNotFoundError(CreateInGameEventCommand command)
        {
            // Arrange
            var validator = new CreateInGameEventCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.CreateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(InvalidInGameIdCreateInGameEventCommands))]
        public async Task HandleCreateInGameEventCommand_InvalidInGameIdCommandGiven_ShouldBeCatchedByValidator(CreateInGameEventCommand command)
        {
            // Arrange
            var validator = new CreateInGameEventCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.CreateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidInGameEventTypeCreateInGameEventCommands))]
        public async Task HandleCreateInGameEventCommand_InvalidInGameEventTypeCommandGiven_ShouldBeCatchedByValidator(CreateInGameEventCommand command)
        {
            // Arrange
            var validator = new CreateInGameEventCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.CreateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEmptyDescriptionCreateInGameEventCommands))]
        public async Task HandleCreateInGameEventCommand_InvalidEmptyDescriptionCommandGiven_ShouldBeCatchedByValidator(CreateInGameEventCommand command)
        {
            // Arrange
            var validator = new CreateInGameEventCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.CreateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidPriceCreateInGameEventCommands))]
        public async Task HandleCreateInGameEventCommand_InvalidPriceCommandGiven_ShouldBeCatchedByValidator(CreateInGameEventCommand command)
        {
            // Arrange
            var validator = new CreateInGameEventCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _inGameEventTestEnvironment.MockInGameEventRepository.Verify(x => x.CreateInGameEvent(It.IsAny<InGameEvent>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidCreateInGameEventCommands()
        {
            var testInGameEventEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();

            yield return new[] { CreateInGameEventCommandUtils.Create() };

            yield return new[] { CreateInGameEventCommandUtils.Create(testInGameEventEnvironment.InGameEvents[0].GameServerId.Value,
                                                                      10005,
                                                                      "PlayerEvent",
                                                                      "Description 1",
                                                                      3000) };

            yield return new[] { CreateInGameEventCommandUtils.Create(testInGameEventEnvironment.InGameEvents[1].GameServerId.Value,
                                                                      10006,
                                                                      "PlayerEvent",
                                                                      "Description 2",
                                                                      3500) };
        }
        public static IEnumerable<object[]> InvalidNotExistingGameServerIdCreateInGameEventCommands()
        {
            yield return new[] { CreateInGameEventCommandUtils.Create(gameServerId: Guid.NewGuid()) };

            yield return new[] { CreateInGameEventCommandUtils.Create(gameServerId: Guid.NewGuid()) };
        }
        public static IEnumerable<object[]> InvalidInGameIdCreateInGameEventCommands()
        {
            yield return new[] { CreateInGameEventCommandUtils.Create(inGameId: 0) };

            yield return new[] { CreateInGameEventCommandUtils.Create(inGameId: -50) };
        }
        public static IEnumerable<object[]> InvalidInGameEventTypeCreateInGameEventCommands()
        {
            yield return new[] { CreateInGameEventCommandUtils.Create(inGameEventType: "FreeMoney") };
            yield return new[] { CreateInGameEventCommandUtils.Create(inGameEventType: "FreeAdminRoles") };
        }

        public static IEnumerable<object[]> InvalidEmptyDescriptionCreateInGameEventCommands()
        {
            yield return new[] { CreateInGameEventCommandUtils.Create(description: "") };
            yield return new[] { CreateInGameEventCommandUtils.Create(description: "") };
        }
        public static IEnumerable<object[]> InvalidPriceCreateInGameEventCommands()
        {
            yield return new[] { CreateInGameEventCommandUtils.Create(price: 0) };
            yield return new[] { CreateInGameEventCommandUtils.Create(price: -3000) };
        }
    }
}
