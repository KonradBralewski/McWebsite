namespace McWebsite.API.Contracts.GameServer
{
    public sealed record CreateGameServerResponse(int MaximumPlayersNumber,
                                                 int CurrentPlayersNumber,
                                                 string ServerLocation,
                                                 string ServerType,
                                                 string Description);
}
