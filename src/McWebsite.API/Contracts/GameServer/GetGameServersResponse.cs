namespace McWebsite.API.Contracts.GameServer
{
    public sealed record GetGameServersResponse(
        List<GameServerResponse> GameServers
        );

    public sealed record GameServerResponse(
        Guid Id,
        string ServerLocation,
        int MaximumPlayersNumber,
        string ServerType,
        string Description);
}
