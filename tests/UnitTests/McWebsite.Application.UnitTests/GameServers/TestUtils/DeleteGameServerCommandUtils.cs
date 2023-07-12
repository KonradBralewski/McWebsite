using McWebsite.Application.GameServers.Commands.DeleteGameServerCommand;
using McWebsite.Application.GameServers.Queries.GetGameServer;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.GameServers.TestUtils
{
    public static class DeleteGameServerCommandUtils
    {
        public static DeleteGameServerCommand Create(Guid? Id = null)
        {
            return new DeleteGameServerCommand(Id ?? Constants.GameServerQueriesAndCommands.Id);
        }
    }
}
