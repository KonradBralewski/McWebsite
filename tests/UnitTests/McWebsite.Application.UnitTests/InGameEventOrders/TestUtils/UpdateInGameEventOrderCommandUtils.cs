using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand;
using McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.InGameEventOrders.TestUtils
{
    public static class UpdateInGameEventOrderCommandUtils
    {
        public static UpdateInGameEventOrderCommand Create(Guid? inGameEventOrderId = null,
                                                           Guid? boughtInGameEventId = null)
        {
            return new UpdateInGameEventOrderCommand(inGameEventOrderId ?? Constants.InGameEventOrderQueriesAndCommands.Id,
                                                     boughtInGameEventId ?? Constants.InGameEventOrderQueriesAndCommands.BoughtInGameEventId);
        }
    }
}
