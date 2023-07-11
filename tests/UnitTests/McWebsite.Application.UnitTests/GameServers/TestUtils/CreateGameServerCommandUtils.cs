﻿using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServers.TestUtils
{
    public static class CreateGameServerCommandUtils
    {
        public static CreateGameServerCommand Create(int? maximumPlayersNumber = null,
                                                     string? serverLocation = null,
                                                     string? serverType = null,
                                                     string? description = null)
        {
            return new CreateGameServerCommand(maximumPlayersNumber ?? Constants.GameServer.MaximumPlayersNumber,
                                               serverLocation ?? Constants.GameServer.ServerLocation,
                                               serverType ?? Constants.GameServer.ServerType,
                                               description ?? Constants.GameServer.Description);
        }
    }
}
