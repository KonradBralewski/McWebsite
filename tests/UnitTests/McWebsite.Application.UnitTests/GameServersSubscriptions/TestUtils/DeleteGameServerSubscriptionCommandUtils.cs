using McWebsite.Application.GameServerSubscriptions.Commands.DeleteGameServerSubscriptionCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;

namespace McWebsite.Application.UnitTests.GameServersSubscriptions.TestUtils
{
    public static class DeleteGameServerSubscriptionCommandUtils
    {
        public static DeleteGameServerSubscriptionCommand Create(Guid? Id = null)
        {
            return new DeleteGameServerSubscriptionCommand(Id ?? Constants.GameServerSubscriptionQueriesAndCommands.Id);
        }
    }
}
