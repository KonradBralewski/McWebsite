using FluentAssertions;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.Enums;

namespace McWebsite.Application.UnitTests.TestUtils.Extensions
{
    public static class GameServerValidationExtensions
    {
        public static void ValidateIfCreatedFrom(this GameServer gameServer, CreateGameServerCommand command)
        {
            gameServer.Id.Should().NotBeNull();

            gameServer.Description.Should().Be(command.Description);

            gameServer.MaximumPlayersNumber.Should().Be(command.MaximumPlayersNumber);

            gameServer.ServerLocation.Value.ToString().Should().BeOneOf(Enum.GetNames(typeof(ServerLocation)));
            gameServer.ServerType.Value.ToString().Should().BeOneOf(Enum.GetNames(typeof(ServerType)));

            gameServer.ServerType.Value.ToString().Should().Be(command.ServerType);
            gameServer.ServerLocation.Value.ToString().Should().Be(command.ServerLocation);
        }
    }
}
