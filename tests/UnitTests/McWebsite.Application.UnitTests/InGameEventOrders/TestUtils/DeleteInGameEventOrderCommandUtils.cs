using McWebsite.Application.GameServers.Queries.GetGameServer;
using McWebsite.Application.InGameEventOrders.Commands.DeleteInGameEventOrderCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.InGameEventOrders.TestUtils
{
    public static class DeleteInGameEventOrderCommandUtils
    {
        public static DeleteInGameEventOrderCommand Create(Guid? Id = null)
        {
            return new DeleteInGameEventOrderCommand(Id ?? Constants.InGameEventOrderQueriesAndCommands.Id);
        }
    }
}
