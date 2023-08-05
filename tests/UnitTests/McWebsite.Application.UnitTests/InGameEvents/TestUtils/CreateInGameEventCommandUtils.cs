using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Application.InGameEvents.Commands.CreateInGameEventCommand;

namespace McWebsite.Application.UnitTests.InGameEvents.TestUtils
{
    public static class CreateInGameEventCommandUtils
    {
        public static CreateInGameEventCommand Create(Guid? gameServerId = null,
                                                      int? inGameId = null,
                                                      string? inGameEventType = null,
                                                      string? description = null,
                                                      float? price = null)
        {
            return new CreateInGameEventCommand(gameServerId ?? Constants.InGameEventQueriesAndCommands.GameServerId,
                                                inGameId ?? Constants.InGameEventQueriesAndCommands.InGameId,
                                                inGameEventType ?? Constants.InGameEventQueriesAndCommands.InGameEventType,
                                                description ?? Constants.InGameEventQueriesAndCommands.Description,
                                                price ?? Constants.InGameEventQueriesAndCommands.Price);
        }
    }
}
