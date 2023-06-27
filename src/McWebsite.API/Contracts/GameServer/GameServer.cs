using McWebsite.Domain.GameServer.ValueObjects;

namespace McWebsite.API.Contracts.GameServer
{
    public sealed record GameServer(
        Guid Id,
        string ServerLocation,
        int MaximumPlayersNumber,
        string ServerType,
        string Description);
}
