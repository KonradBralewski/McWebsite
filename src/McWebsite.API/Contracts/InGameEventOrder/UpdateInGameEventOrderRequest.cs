namespace McWebsite.API.Contracts.InGameEventOrder
{
    public sealed record UpdateInGameEventOrderRequest(Guid BuyingUserId,
                                                       Guid BoughtInGameEventId);
}
