using McWebsite.Domain.InGameEventOrder;

namespace McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrdersQuery
{
    public sealed record GetInGameEventOrdersResult(IEnumerable<InGameEventOrder> InGameEventOrders);
}
