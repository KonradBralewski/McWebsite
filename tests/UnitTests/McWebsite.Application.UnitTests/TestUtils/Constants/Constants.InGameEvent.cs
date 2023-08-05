using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class InGameEventQueriesAndCommands
        {
            public static int Page = 0;
            public static int EntriesPerPage = 50;

            public static Guid Id = Guid.NewGuid();
            public static Guid GameServerId = GameServerQueriesAndCommands.Id;
            public static int InGameId = 12004;
            public static string InGameEventType = "EntityEvent";
            public static string Description = "Test description.";
            public static float Price = 5000;
        }


    }
}
