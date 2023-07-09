namespace McWebsite.API.Contracts.GameServer
{
    public sealed record UpdateGameServerRequest(int MaximumPlayersNumber,
                                                 int CurrentPlayersNumber,
                                                 string ServerLocation,
                                                 string ServerType,
                                                 string Description);
}
