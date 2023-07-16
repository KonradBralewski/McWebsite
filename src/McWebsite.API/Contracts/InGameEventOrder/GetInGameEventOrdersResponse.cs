namespace McWebsite.API.Contracts.InGameEventOrder
{
    public sealed record GetInGameEventOrdersResponse(IEnumerable<GetInGameEventOrderResponse> InGameEventOrders);
}
