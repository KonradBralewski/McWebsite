﻿namespace McWebsite.API.Contracts.InGameEvent
{
    public sealed record CreateInGameEventResponse(Guid Id,
                                                   Guid GameServerId,
                                                   int InGameId,
                                                   string InGameEventType,
                                                   string Description,
                                                   float Price,
                                                   DateTime CreatedDateTime);
}
