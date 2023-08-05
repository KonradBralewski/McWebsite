using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class InGameEventOrderQueriesAndCommands
        {
            public static int Page = 0;
            public static int EntriesPerPage = 50;

            public static Guid Id = Guid.NewGuid();
            public static Guid BuyingUserId = Guid.NewGuid();
            public static Guid BoughtInGameEventId = InGameEventQueriesAndCommands.Id;
        }


    }
}
