using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Application.InGameEvents.Commands.DeleteInGameEventCommand;

namespace McWebsite.Application.UnitTests.InGameEvents.TestUtils
{
    public static class DeleteInGameEventCommandUtils
    {
        public static DeleteInGameEventCommand Create(Guid? Id = null)
        {
            return new DeleteInGameEventCommand(Id ?? Constants.InGameEventQueriesAndCommands.Id);
        }
    }
}
