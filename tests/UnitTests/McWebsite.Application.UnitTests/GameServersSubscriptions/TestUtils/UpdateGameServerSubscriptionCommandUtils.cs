using McWebsite.Application.GameServerSubscriptions.Commands.UpdateGameServerSubscriptionCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServersSubscriptions.TestUtils
{
    public static class UpdateGameServerSubscriptionCommandUtils
    {
        public static UpdateGameServerSubscriptionCommand Create(Guid? id = null,
                                                                 Guid? gameServerId = null,
                                                                 string? subscriptionType = null,
                                                                 int? inGameSubscriptionId = null,
                                                                 float? price = null,
                                                                 string? subscriptionDescription = null,
                                                                 TimeSpan? subscriptionDuration = null)
        {
            return new UpdateGameServerSubscriptionCommand(id ?? Constants.GameServerSubscriptionQueriesAndCommands.Id,
                                               gameServerId ?? Constants.GameServerSubscriptionQueriesAndCommands.GameServerId,
                                               subscriptionType ?? Constants.GameServerSubscriptionQueriesAndCommands.SubscriptionType,
                                               inGameSubscriptionId ?? Constants.GameServerSubscriptionQueriesAndCommands.InGameSubscriptionId,
                                               price ?? Constants.GameServerSubscriptionQueriesAndCommands.Price,
                                               subscriptionDescription ?? Constants.GameServerSubscriptionQueriesAndCommands.SubscriptionDescription,
                                               subscriptionDuration ?? Constants.GameServerSubscriptionQueriesAndCommands.SubscriptionDuration);
        }
    }
}
