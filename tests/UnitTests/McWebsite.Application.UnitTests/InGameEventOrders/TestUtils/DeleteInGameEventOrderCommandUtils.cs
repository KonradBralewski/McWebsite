using McWebsite.Application.InGameEventOrders.Commands.DeleteInGameEventOrderCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.InGameEventOrders.TestUtils
{
    public static class DeleteInGameEventOrderCommandUtils
    {
        public static DeleteInGameEventOrderCommand Create(Guid? Id = null)
        {
            return new DeleteInGameEventOrderCommand(Id ?? Constants.InGameEventOrderQueriesAndCommands.Id);
        }
    }
}
