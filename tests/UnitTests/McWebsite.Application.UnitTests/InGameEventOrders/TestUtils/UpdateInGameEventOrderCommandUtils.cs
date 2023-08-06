using McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.InGameEventOrders.TestUtils
{
    public static class UpdateInGameEventOrderCommandUtils
    {
        public static UpdateInGameEventOrderCommand Create(Guid? inGameEventOrderId = null,
                                                           Guid? boughtInGameEventId = null)
        {
            return new UpdateInGameEventOrderCommand(inGameEventOrderId ?? Constants.InGameEventOrderQueriesAndCommands.Id,
                                                     boughtInGameEventId ?? Constants.InGameEventOrderQueriesAndCommands.BoughtInGameEventId);
        }
    }
}
