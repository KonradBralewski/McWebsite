using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerSubscription.Enums;
using McWebsite.Domain.GameServerSubscription.ValueObjects;


namespace McWebsite.Domain.GameServerSubscription
{
    public sealed class GameServerSubscription : AggregateRoot<GameServerSubscriptionId, Guid>
    {

        public GameServerId GameServerId { get; private set; }
        
        public GameServerSubscriptionType SubscriptionType { get; private set; }
        public int InGameSubscriptionId { get; private set; }
        public float Price { get; private set; }
        public string SubscriptionDescription { get; private set; }
        public TimeSpan SubscriptionDuration { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }
        private GameServerSubscription(GameServerSubscriptionId id,
                                GameServerId gameServerId,
                                GameServerSubscriptionType subscriptionType,
                                int inGameSubscriptionId,
                                float price,
                                string subscriptionDescription,
                                TimeSpan subscriptionDuration,
                                DateTime createdDateTime,
                                DateTime updatedDateTime) : base(id)
        {
            Id = id;
            GameServerId = gameServerId;
            SubscriptionType = subscriptionType;
            InGameSubscriptionId = inGameSubscriptionId;
            Price = price;
            SubscriptionDescription = subscriptionDescription;
            SubscriptionDuration = subscriptionDuration;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }
        public static GameServerSubscription Create(Guid gameServerId,
                                SubscriptionType subscriptionType,
                                int inGameSubscriptionId,
                                float price,
                                string subscriptionDescription,
                                TimeSpan subscriptionDuration,
                                DateTime createdDateTime,
                                DateTime subscriptionEndDate)
        {
            return new GameServerSubscription(GameServerSubscriptionId.CreateUnique(),
                                        GameServerId.Create(gameServerId),
                                        GameServerSubscriptionType.Create(subscriptionType),
                                        inGameSubscriptionId,
                                        price,
                                        subscriptionDescription,
                                        subscriptionDuration,
                                        createdDateTime,
                                        subscriptionEndDate);
        }

        public static GameServerSubscription Recreate(Guid id,
                                                      Guid gameServerId,
                                                      SubscriptionType subscriptionType,
                                                      int inGameSubscriptionId,
                                                      float price,
                                                      string subscriptionDescription,
                                                      TimeSpan subscriptionDuration,
                                                      DateTime createdDateTime,
                                                      DateTime subscriptionEndDate)
        {
            return new GameServerSubscription(GameServerSubscriptionId.Create(id),
                                              GameServerId.Create(gameServerId),
                                              GameServerSubscriptionType.Create(subscriptionType),
                                              inGameSubscriptionId,
                                              price,
                                              subscriptionDescription,
                                              subscriptionDuration,
                                              createdDateTime,
                                              subscriptionEndDate);
        }

        /// <summary>
        /// Constructor that will be used by EF Core, EF Core is not able to setup navigation property for Tuple<UserId, UserId>
        /// </summary>
#pragma warning disable CS8618
        private GameServerSubscription()
        {

        }

#pragma warning restore CS8618
    }
}
