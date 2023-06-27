namespace McWebsite.API.Contracts.GameServer
{
    public sealed record GetGameServersResponse(
        List<GameServer> GameServers
        );
}
