namespace McWebsite.API.Contracts.GameServer
{
    public sealed record CreateGameServerResponse(Guid Id,
                                                  int MaximumPlayersNumber,
                                                  string ServerLocation,
                                                  string ServerType,
                                                  string Description,
                                                  DateTime CreatedDateTime);
}
