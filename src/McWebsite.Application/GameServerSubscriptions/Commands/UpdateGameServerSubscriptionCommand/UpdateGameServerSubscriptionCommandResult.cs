using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerSubscription;

namespace McWebsite.Application.GameServerSubscriptions.Commands.UpdateGameServerSubscriptionCommand
{
    public sealed record UpdateGameServerSubscriptionResult(GameServerSubscription GameServerSubscription);
}
