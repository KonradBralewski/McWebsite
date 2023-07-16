namespace McWebsite.API.Contracts.InGameEventOrder
{
    public sealed record GetInGameEventOrderResponse(Guid Id,
                                                     Guid BuyingUserId,
                                                     Guid BoughtInGameEventId);
}
