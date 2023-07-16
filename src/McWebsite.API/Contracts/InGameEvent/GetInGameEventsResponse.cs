namespace McWebsite.API.Contracts.InGameEvent
{
    public sealed record GetInGameEventsResponse(IEnumerable<GetInGameEventResponse> InGameEvents);
}
