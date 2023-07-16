using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;

namespace McWebsite.API.Contracts.InGameEvent
{
    public sealed record CreateInGameEventRequest(Guid GameServerId,
                                                  int InGameId,
                                                  string InGameEventType,
                                                  string Description,
                                                  float Price);
}
