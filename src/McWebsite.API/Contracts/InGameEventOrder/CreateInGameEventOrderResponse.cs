namespace McWebsite.API.Contracts.InGameEventOrder
{
    public sealed record CreateInGameEventOrderResponse(Guid Id,
                                                        Guid BuyingUserId,
                                                        Guid BoughtInGameEventId,
                                                        DateTime OrderDate);
}
