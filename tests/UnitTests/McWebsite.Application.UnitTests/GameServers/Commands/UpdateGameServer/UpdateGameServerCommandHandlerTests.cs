using FluentAssertions;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.GameServers.TestUtils;
using McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerExtensions;
using McWebsite.Domain.GameServer;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServers.Commands.UpdateGameServer
{
    public sealed class UpdateGameServerCommandHandlerTests
    {
        private readonly UpdateGameServerCommandHandler _handler;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _testEnvironment;
        public UpdateGameServerCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();
            _handler = new UpdateGameServerCommandHandler(_testEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidUpdateGameServerCommands))]
        public async Task HandleUpdateGameServerCommand_ValidCommandGiven_ShouldReturnAndUpdateGameServer(UpdateGameServerCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeFalse();
            var gameServer = commandResult.Value.Value.GameServer; // Nullable object (ErrorOr<UpdateGameServerResult>?)
            gameServer.Should().NotBeNull();
            gameServer.ValidateIfUpdatedFrom(command);
            _testEnvironment.MockGameServerRepository.Verify(x => x.UpdateGameServer(It.IsAny<GameServer>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ValidButWithNoChangesUpdateGameServerCommands))]
        public async Task HandleUpdateGameServerCommand_ValidCommandGivenButWithNoChangeIsNeeded_ShouldReturnNull(UpdateGameServerCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Should().BeNull();
            _testEnvironment.MockGameServerRepository.Verify(x => x.UpdateGameServer(It.IsAny<GameServer>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(NotExistingIdUpdateGameServerCommands))]
        public async Task HandleUpdateGameServerCommand_NotExistingIdCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.HasValue.Should().BeTrue();
            commandResult.Value.IsError.Should().BeTrue();
            commandResult.Value.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockGameServerRepository.Verify(x => x.UpdateGameServer(It.IsAny<GameServer>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidMaximumPlayersNumberUpdateGameServerCommands))]
        public async Task HandleUpdateGameServerCommand_InvalidMaximumPlayersNumberCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerRepository.Verify(x => x.UpdateGameServer(It.IsAny<GameServer>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidServerLocationUpdateGameServerCommands))]
        public async Task HandleUpdateGameServerCommand_InvalidServerLocationCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerRepository.Verify(x => x.UpdateGameServer(It.IsAny<GameServer>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidServerTypeUpdateGameServerCommands))]
        public async Task HandleUpdateGameServerCommand_InvalidServerTypeCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerRepository.Verify(x => x.UpdateGameServer(It.IsAny<GameServer>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEmptyDescriptionCreateGameServerCommands))]
        public async Task HandleUpdateGameServerCommand_InvalidEmptyDescriptionCommandGiven_ShouldBeCatchedByValidator(UpdateGameServerCommand command)
        {
            // Arrange
            var validator = new UpdateGameServerCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerRepository.Verify(x => x.UpdateGameServer(It.IsAny<GameServer>()), Times.Never);
        }
        public static IEnumerable<object[]> ValidUpdateGameServerCommands()
        {
            var testEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();

            yield return new[] { UpdateGameServerCommandUtils.Create(testEnvironment.GameServers[0].Id.Value,
                                                                     1200,
                                                                     300,
                                                                     "Africa",
                                                                     "Bukkit",
                                                                     "New Description 1") };

            yield return new[] { UpdateGameServerCommandUtils.Create(testEnvironment.GameServers[1].Id.Value,
                                                                     2000,
                                                                     500,
                                                                     "Europe",
                                                                     "Vanilla",
                                                                     "New Description 2") };
        }

        public static IEnumerable<object[]> ValidButWithNoChangesUpdateGameServerCommands()
        {
            var testEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();
            GameServer firstGameServer = testEnvironment.GameServers.ElementAt(0);
            GameServer secondGameServer = testEnvironment.GameServers.ElementAt(1);

            yield return new[] { UpdateGameServerCommandUtils.Create(firstGameServer.Id.Value,
                                                                     firstGameServer.MaximumPlayersNumber,
                                                                     firstGameServer.CurrentPlayersNumber,
                                                                     firstGameServer.ServerLocation.Value.ToString(),
                                                                     firstGameServer.ServerType.Value.ToString(),
                                                                     firstGameServer.Description) };

            yield return new[] { UpdateGameServerCommandUtils.Create(secondGameServer.Id.Value,
                                                                     secondGameServer.MaximumPlayersNumber,
                                                                     secondGameServer.CurrentPlayersNumber,
                                                                     secondGameServer.ServerLocation.Value.ToString(),
                                                                     secondGameServer.ServerType.Value.ToString(),
                                                                     secondGameServer.Description) };
        }
        public static IEnumerable<object[]> NotExistingIdUpdateGameServerCommands()
        {
            yield return new[] { UpdateGameServerCommandUtils.Create(Guid.NewGuid()) };

            yield return new[] { UpdateGameServerCommandUtils.Create(Guid.NewGuid()) };
        }
        public static IEnumerable<object[]> InvalidMaximumPlayersNumberUpdateGameServerCommands()
        {
            yield return new[] { UpdateGameServerCommandUtils.Create(maximumPlayersNumber: 0) };

            yield return new[] { UpdateGameServerCommandUtils.Create(maximumPlayersNumber: -300) };
        }
        public static IEnumerable<object[]> InvalidServerLocationUpdateGameServerCommands()
        {
            yield return new[] { UpdateGameServerCommandUtils.Create(serverType: "Italy") };
            yield return new[] { UpdateGameServerCommandUtils.Create(serverType: "Naples") };
        }

        public static IEnumerable<object[]> InvalidServerTypeUpdateGameServerCommands()
        {
            yield return new[] { UpdateGameServerCommandUtils.Create(serverType: "CS_SOURCE") };
            yield return new[] { UpdateGameServerCommandUtils.Create(serverType: "CS_2") };
        }
        public static IEnumerable<object[]> InvalidEmptyDescriptionCreateGameServerCommands()
        {
            yield return new[] { UpdateGameServerCommandUtils.Create(description: "") };
            yield return new[] { UpdateGameServerCommandUtils.Create(description: "") };
        }
    }
}
