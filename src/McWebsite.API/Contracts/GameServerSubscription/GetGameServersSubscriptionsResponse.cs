namespace McWebsite.API.Contracts.GameServerSubscription
{
    public sealed record GetGameServersSubscriptionsResponse(IEnumerable<GetGameServerSubscriptionResponse> GameServersSubscriptions);
}
