﻿namespace McWebsite.API.Contracts.GameServer
{
    public sealed record UpdateGameServerResponse(Guid Id,
                                                  int MaximumPlayersNumber,
                                                  int CurrentPlayersNumber,
                                                  string ServerLocation,
                                                  string ServerType,
                                                  string Description);
}
