using McWebsite.Domain.InGameEvent.Entities;

namespace McWebsite.Application.InGameEvents.Queries.GetInGameEventsQuery
{
    public sealed record GetInGameEventsResult(IEnumerable<InGameEvent> InGameEvents);
}
