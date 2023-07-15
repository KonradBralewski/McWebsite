namespace McWebsite.API.Contracts.GameServerSubscription
{
    public sealed record GetGameServerSubscriptionResponse(Guid Id,
                                                           Guid GameServerId,
                                                           string SubscriptionType,
                                                           int InGameSubscriptionId,
                                                           float Price,
                                                           string SubscriptionDescription,
                                                           TimeSpan SubscriptionDuration,
                                                           DateTime CreatedDateTime);
}
