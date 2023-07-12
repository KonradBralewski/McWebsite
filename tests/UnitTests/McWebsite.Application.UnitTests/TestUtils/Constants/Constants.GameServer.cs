using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class GameServer 
        {
            
            public static int MaximumPlayersNumber = 1000;
            public static int CurrentPlayersNumber = 1000;
            public static string ServerLocation = "Europe";
            public static string ServerType = "Vanilla";
            public static string Description = "Test Description";
        }

        public static class GameServerQueriesAndCommands
        {
            public static Guid Id = Guid.NewGuid();
            public static int Page = 0;
            public static int EntriesPerPage = 50;
        }


    }
}
