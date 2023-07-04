using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using McWebsite.Domain.User.ValueObjects;


namespace McWebsite.Domain.GameServerSubscription
{
    public sealed class GameServerSubscription : AggregateRoot<GameServerSubscriptionId>
    {

        public GameServerId GameServerId { get; private set; }
        public UserId BuyingPlayerId { get; private set; }
        public string SubscriptionDescription { get; private set; }
        public DateTime SubscriptionStartDate { get; private set; }
        public DateTime SubscriptionEndDate { get; private set; }

        public DateTime UpdatedDateTime { get; private set; }
        public GameServerSubscription(GameServerSubscriptionId id,
                                GameServerId gameServerId,
                                UserId buyingPlayerId,
                                string subscriptionDescription,
                                DateTime subscriptionStartDate,
                                DateTime subscriptionEndDate,
                                DateTime updatedDateTime) : base(id)
        {
            Id = id;
            GameServerId = gameServerId;
            BuyingPlayerId = buyingPlayerId;
            SubscriptionDescription = subscriptionDescription;
            SubscriptionStartDate = subscriptionStartDate;
            SubscriptionEndDate = subscriptionEndDate;
            UpdatedDateTime = updatedDateTime;
        }
        public static GameServerSubscription Create(Guid gameServerId,
                                Guid buyingPlayerId,
                                string subscriptionDescription,
                                DateTime subscriptionStartDate,
                                DateTime subscriptionEndDate,
                                DateTime updatedDateTime)
        {
            return new GameServerSubscription(GameServerSubscriptionId.CreateUnique(),
                                        GameServerId.Create(gameServerId),
                                        UserId.Create(buyingPlayerId),
                                        subscriptionDescription,
                                        subscriptionStartDate,
                                        subscriptionEndDate,
                                        updatedDateTime);
        }
    }
}
