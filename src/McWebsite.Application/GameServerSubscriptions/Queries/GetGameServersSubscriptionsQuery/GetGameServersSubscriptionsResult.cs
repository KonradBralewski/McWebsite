using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerSubscription;

namespace McWebsite.Application.GameServerSubscriptions.Queries.GetGameServersSubscriptionsQuery
{
    public sealed record GetGameServersSubscriptionsResult(IEnumerable<GameServerSubscription> GameServersSubscriptions);
}
