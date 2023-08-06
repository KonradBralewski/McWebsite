using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {

        public static class GameServerSubscriptionQueriesAndCommands
        {
            public static int Page = 0;
            public static int EntriesPerPage = 50;

            public static Guid Id = Guid.NewGuid();
            public static Guid GameServerId = GameServerQueriesAndCommands.Id;
            public static string SubscriptionType = "ResourcePackExclusive";
            public static int InGameSubscriptionId = 12006;
            public static float Price = 4500;
            public static string SubscriptionDescription = "Test Description";
            public static TimeSpan SubscriptionDuration = TimeSpan.FromDays(15);
        }


    }
}
