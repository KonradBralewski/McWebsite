using FluentAssertions;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.Enums;

namespace McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerExtensions
{
    public static class GameServerValidationExtensions
    {
        public static void ValidateIfCreatedFrom(this GameServer gameServer, CreateGameServerCommand command)
        {
            gameServer.Id.Value.ToString().Should().NotBeEmpty();

            gameServer.Description.Should().Be(command.Description);

            gameServer.MaximumPlayersNumber.Should().Be(command.MaximumPlayersNumber);

            gameServer.ServerType.Value.ToString().Should().Be(command.ServerType);

            gameServer.ServerLocation.Value.ToString().Should().Be(command.ServerLocation);
        }

        public static void ValidateIfUpdatedFrom(this GameServer gameServer, UpdateGameServerCommand command)
        {
            gameServer.Id.Value.Should().Be(command.GameServerId);

            gameServer.Description.Should().Be(command.Description);

            gameServer.MaximumPlayersNumber.Should().Be(command.MaximumPlayersNumber);

            gameServer.CurrentPlayersNumber.Should().Be(command.CurrentPlayersNumber);

            gameServer.ServerType.Value.ToString().Should().Be(command.ServerType);

            gameServer.ServerLocation.Value.ToString().Should().Be(command.ServerLocation);
        }
    }
}
