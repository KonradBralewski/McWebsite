using McWebsite.Application.GameServers.Queries.GetGameServer;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.GameServers.TestUtils
{
    public static class GetGameServerQueryUtils
    {
        public static GetGameServerQuery Create(Guid? Id = null)
        {
            return new GetGameServerQuery(Id ?? Constants.GameServerQueriesAndCommands.Id);
        }
    }
}
