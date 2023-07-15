using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {

        public static class GameServerReportQueriesAndCommands
        {
            public static int Page = 0;
            public static int EntriesPerPage = 50;
            public static Guid ReportingUserId = Guid.NewGuid();

            public static Guid Id = Guid.NewGuid();
            public static Guid ReportedGameServerId = GameServerQueriesAndCommands.Id;
            public static string ReportType = "DataLoss";
            public static string Description = "Test Description";
        }


    }
}
