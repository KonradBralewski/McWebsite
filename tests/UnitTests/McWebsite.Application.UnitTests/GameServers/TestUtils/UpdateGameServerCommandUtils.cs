using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.GameServers.TestUtils
{
    public static class UpdateGameServerCommandUtils
    {
        public static UpdateGameServerCommand Create(Guid? id = null,
                                                     int? maximumPlayersNumber = null,
                                                     int? currentPlayersNumber = null,
                                                     string? serverLocation = null,
                                                     string? serverType = null,
                                                     string? description = null)
        {
            return new UpdateGameServerCommand(id ?? Constants.GameServerQueriesAndCommands.Id,
                                               maximumPlayersNumber ?? Constants.GameServer.MaximumPlayersNumber,
                                               currentPlayersNumber ?? Constants.GameServer.CurrentPlayersNumber,
                                               serverLocation ?? Constants.GameServer.ServerLocation,
                                               serverType ?? Constants.GameServer.ServerType,
                                               description ?? Constants.GameServer.Description);
        }
    }
}
