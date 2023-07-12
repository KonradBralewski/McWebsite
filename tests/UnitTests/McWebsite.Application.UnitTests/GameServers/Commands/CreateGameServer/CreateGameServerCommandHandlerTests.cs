using FluentAssertions;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.UnitTests.GameServers.TestEnvironments;
using McWebsite.Application.UnitTests.GameServers.TestUtils;
using McWebsite.Application.UnitTests.TestUtils.Extensions;
using McWebsite.Domain.GameServer;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServers.Commands.CreateGameServer
{
    public sealed class CreateGameServerCommandHandlerTests
    {
        private readonly CreateGameServerCommandHandler _handler;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _testEnvironment;
        public CreateGameServerCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();
            _handler = new CreateGameServerCommandHandler(_testEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidCreateGameServerCommands))]
        public async Task HandleCreateGameServerCommand_ValidCommandGiven_ShouldReturnAndCreateGameServer(CreateGameServerCommand command)
        {
            // Arrange
            var validator = new CreateGameServerCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.GameServer.Should().NotBeNull();
            commandResult.IsError.Should().BeFalse();
            commandResult.Value.GameServer.ValidateIfCreatedFrom(command);
            _testEnvironment.MockGameServerRepository.Verify(x => x.CreateGameServer(It.IsAny<GameServer>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidLocationCreateGameServerCommands))]
        public async Task HandleCreateGameServerCommand_InvalidLocationCommandGiven_ShouldBeCatchedByValidator(CreateGameServerCommand command)
        {
            // Arrange
            var validator = new CreateGameServerCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerRepository.Verify(x => x.CreateGameServer(It.IsAny<GameServer>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(InvalidTypeCreateGameServerCommands))]
        public async Task HandleCreateGameServerCommand_InvalidServerTypeCommandGiven_ShouldBeCatchedByValidator(CreateGameServerCommand command)
        {
            // Arrange
            var validator = new CreateGameServerCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerRepository.Verify(x => x.CreateGameServer(It.IsAny<GameServer>()), Times.Never);
        }
        [Theory]
        [MemberData(nameof(InvalidEmptyDescriptionCreateGameServerCommands))]
        public async Task HandleCreateGameServerCommand_InvalidEmptyDescriptionCommandGiven_ShouldBeCatchedByValidator(CreateGameServerCommand command)
        {
            // Arrange
            var validator = new CreateGameServerCommandValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerRepository.Verify(x => x.CreateGameServer(It.IsAny<GameServer>()), Times.Never);
        }
        public static IEnumerable<object[]>ValidCreateGameServerCommands()
        {
            yield return new[] { CreateGameServerCommandUtils.Create() };
            yield return new[] { CreateGameServerCommandUtils.Create(1200, "Africa", "Bukkit", "New Description") };
            yield return new[] { CreateGameServerCommandUtils.Create(2500, "Asia", "Paper", "New Description") };
        }

        public static IEnumerable<object[]> InvalidLocationCreateGameServerCommands()
        {
            yield return new[] { CreateGameServerCommandUtils.Create(1200, "_Africa", "Bukkit", "New Description") };
            yield return new[] { CreateGameServerCommandUtils.Create(2500, "_Asia", "Paper", "New Description") };
        }

        public static IEnumerable<object[]> InvalidTypeCreateGameServerCommands()
        {
            yield return new[] { CreateGameServerCommandUtils.Create(1200, "Africa", "CSGO", "New Description") };
            yield return new[] { CreateGameServerCommandUtils.Create(2500, "Asia", "CS_SOURCE", "New Description") };
        }
        public static IEnumerable<object[]> InvalidEmptyDescriptionCreateGameServerCommands()
        {
            yield return new[] { CreateGameServerCommandUtils.Create(1200, "Africa", "Bukkit", "") };
            yield return new[] { CreateGameServerCommandUtils.Create(2500, "Asia", "Paper", "") };
        }
    }
}
