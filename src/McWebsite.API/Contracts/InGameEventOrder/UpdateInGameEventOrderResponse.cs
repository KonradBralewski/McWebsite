namespace McWebsite.API.Contracts.InGameEventOrder
{
    public sealed record UpdateInGameEventOrderResponse(Guid Id,
                                                     Guid BuyingUserId,
                                                     Guid BoughtInGameEventId,
                                                     DateTime UpdatedDateTime);
}
