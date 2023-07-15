using FluentAssertions;
using McWebsite.Application.GameServers.Commands.DeleteGameServerCommand;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.GameServers.TestUtils;
using McWebsite.Domain.GameServer;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServers.Commands.DeleteGameServer
{
    public sealed class DeleteGameServerCommandHandlerTests
    {
        private readonly DeleteGameServerCommandHandler _handler;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _testEnvironment;
        public DeleteGameServerCommandHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();
            _handler = new DeleteGameServerCommandHandler(_testEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdsDeleteGameServerCommands))]
        public async Task HandleDeleteGameServerCommand_ValidIdCommandGiven_ShouldDeleteGameServerAndReturnTrue(DeleteGameServerCommand command)
        {
            // Arrange
            var validator = new DeleteGameServerCommandValidator();
            int existingGameServersCount = _testEnvironment.GameServers.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.Should().BeTrue();
            commandResult.IsError.Should().BeFalse();
            _testEnvironment.MockGameServerRepository.Verify(x => x.DeleteGameServer(It.IsAny<GameServer>()), Times.Once);
            _testEnvironment.GameServers.Count().Should().Be(existingGameServersCount-1);
        }

        [Theory]
        [MemberData(nameof(InvalidIdsDeleteGameServerCommands))]
        public async Task HandleDeleteGameServerCommand_InvalidIdCommandGiven_ShouldReturnNotFoundError(DeleteGameServerCommand command)
        {
            // Arrange
            var validator = new DeleteGameServerCommandValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(command);
            validatorResult.IsValid.Should().BeTrue();

            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.IsError.Should().BeTrue();
            commandResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockGameServerRepository.Verify(x => x.DeleteGameServer(It.IsAny<GameServer>()), Times.Never);
        }
       
        public static IEnumerable<object[]> ValidIdsDeleteGameServerCommands()
        {
            var testEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();
            foreach (GameServer gs in testEnvironment.GameServers)
            {
                yield return new[] { DeleteGameServerCommandUtils.Create(gs.Id.Value) };
            }
        }
        public static IEnumerable<object[]> InvalidIdsDeleteGameServerCommands()
        {
            yield return new[] { DeleteGameServerCommandUtils.Create(Guid.NewGuid()) };
            yield return new[] { DeleteGameServerCommandUtils.Create(Guid.NewGuid()) };
        }
    }
}
