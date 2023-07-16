namespace McWebsite.API.Contracts.InGameEvent
{
    public sealed record GetInGameEventResponse(Guid Id,
                                                Guid GameServerId,
                                                int InGameId,
                                                string InGameEventType,
                                                string Description,
                                                float Price);
}
