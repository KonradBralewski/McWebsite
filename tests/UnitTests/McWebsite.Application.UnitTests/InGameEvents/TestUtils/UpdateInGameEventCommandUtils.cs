using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McWebsite.Application.UnitTests.GameServers.TestUtils;
using McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand;

namespace McWebsite.Application.UnitTests.InGameEvents.TestUtils
{
    public static class UpdateInGameEventCommandUtils
    {
        public static UpdateInGameEventCommand Create(Guid? inGameEventId = null,
                                                      Guid? gameServerId = null,
                                                      int? inGameId = null,
                                                      string? inGameEventType = null,
                                                      string? description = null,
                                                      float? price = null)
        {
            return new UpdateInGameEventCommand(inGameEventId ?? Constants.InGameEventQueriesAndCommands.Id,
                                                gameServerId ?? Constants.InGameEventQueriesAndCommands.GameServerId,
                                                inGameId ?? Constants.InGameEventQueriesAndCommands.InGameId,
                                                inGameEventType ?? Constants.InGameEventQueriesAndCommands.InGameEventType,
                                                description ?? Constants.InGameEventQueriesAndCommands.Description,
                                                price ?? Constants.InGameEventQueriesAndCommands.Price);
        }
    }
}
