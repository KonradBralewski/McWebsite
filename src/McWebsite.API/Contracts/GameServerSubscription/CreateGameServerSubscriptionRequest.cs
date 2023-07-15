namespace McWebsite.API.Contracts.GameServerSubscription
{
    public sealed record CreateGameServerSubscriptionRequest(Guid GameServerId,
                                                             string SubscriptionType,
                                                             int InGameSubscriptionId,
                                                             float Price,
                                                             string SubscriptionDescription,
                                                             TimeSpan SubscriptionDuration);
}
