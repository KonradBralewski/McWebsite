namespace McWebsite.API.Contracts.GameServer
{
    public sealed record GetGameServersResponse(
        IEnumerable<GetGameServerResponse> GameServers
        );
}
