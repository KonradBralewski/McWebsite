using McWebsite.Application.GameServers.Queries.GetGameServer;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.GameServers.TestUtils
{
    public static class GetGameServersQueryUtils
    {
        public static GetGameServersQuery Create(int? page = null, int? entriesPerPage = null)
        {
            return new GetGameServersQuery(page ?? Constants.GameServerQueriesAndCommands.Page,
                                           entriesPerPage ?? Constants.GameServerQueriesAndCommands.EntriesPerPage);
        }
    }
}
