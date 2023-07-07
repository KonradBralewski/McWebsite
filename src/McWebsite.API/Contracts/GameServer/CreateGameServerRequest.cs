using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;

namespace McWebsite.API.Contracts.GameServer
{
    public sealed record CreateGameServerRequest(int MaximumPlayersNumber,
                                                 string ServerLocation,
                                                 string ServerType,
                                                 string Description);
}
