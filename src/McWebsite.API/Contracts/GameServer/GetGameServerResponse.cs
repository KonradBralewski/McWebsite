namespace McWebsite.API.Contracts.GameServer
{
    public sealed record GetGameServerResponse(
    Guid Id,
    string ServerLocation,
    int MaximumPlayersNumber,
    string ServerType,
    string Description);
}
