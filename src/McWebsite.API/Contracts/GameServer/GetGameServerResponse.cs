namespace McWebsite.API.Contracts.GameServer
{
    public sealed record GetGameServerResponse(Guid Id,
                                               int MaximumPlayersNumber,
                                               int CurrentPlayersNumber,
                                               string ServerLocation,
                                               string ServerType,
                                               string Description);
}
