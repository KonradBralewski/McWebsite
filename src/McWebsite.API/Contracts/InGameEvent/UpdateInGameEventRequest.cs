namespace McWebsite.API.Contracts.InGameEvent
{
    public sealed record UpdateInGameEventRequest(Guid GameServerId,
                                                  int InGameId,
                                                  string InGameEventType,
                                                  string Description,
                                                  float Price);
}
