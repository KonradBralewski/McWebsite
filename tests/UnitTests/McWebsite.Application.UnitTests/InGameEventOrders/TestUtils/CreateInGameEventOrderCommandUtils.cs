using McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.InGameEventOrders.TestUtils
{
    public static class CreateInGameEventOrderCommandUtils
    {
        public static CreateInGameEventOrderCommand Create(Guid? buyingUserId = null,
                                                           Guid? boughtInGameEventId = null)
        {
            return new CreateInGameEventOrderCommand(buyingUserId ?? Constants.InGameEventOrderQueriesAndCommands.BuyingUserId,
                                                     boughtInGameEventId ?? Constants.InGameEventOrderQueriesAndCommands.BoughtInGameEventId);
        }
    }
}
