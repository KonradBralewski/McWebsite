using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Domain.InGameEvent.Entities;

namespace McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand
{
    public sealed record UpdateInGameEventResult(InGameEvent InGameEvent);
}
