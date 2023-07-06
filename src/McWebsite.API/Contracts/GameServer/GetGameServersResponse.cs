namespace McWebsite.API.Contracts.GameServer
{
    public sealed record GetGameServersResponse(
        List<GetGameServerResponse> GameServers
        );
}
