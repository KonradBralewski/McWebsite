using FluentAssertions;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.UnitTests.GameServers.TestMocks;
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
        private readonly Mock<IGameServerRepository> _mockGameServerRepository;
        public CreateGameServerCommandHandlerTests()
        {
            _mockGameServerRepository = OurMocks.MockIGameServerRepository.GetMock();
            _handler = new CreateGameServerCommandHandler(_mockGameServerRepository.Object);
        }
        [Fact]
        public async Task HandleCreateGameServerCommand_ValidCommandGiven_ShouldCreateAndReturnGameServer()
        {
            // Arrange
            var command = CreateGameServerCommandUtils.Create();

            // Act
            var commandResult = await _handler.Handle(command, default);

            // Assert
            commandResult.Value.GameServer.Should().NotBeNull();
            commandResult.IsError.Should().BeFalse();
            commandResult.Value.GameServer.ValidateIfCreatedFrom(command);
            _mockGameServerRepository.Verify(x => x.CreateGameServer(It.IsAny<GameServer>()), Times.Once);
        }
    }
}
