using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerSubscription;

namespace McWebsite.Application.GameServerSubscriptions.Queries.GetGameServerSubscriptionQuery
{
    public sealed record GetGameServerSubscriptionResult(GameServerSubscription GameServerSubscription);
}
