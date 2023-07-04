using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServerSubscription
{
    public sealed class GameServerSubscription : AggregateRoot<GameServerSubscriptionId>
    {

        public GameServerId GameServerId { get; private set; }
        public UserId BuyingPlayerId { get; private set; }
        public string SubscriptionDescription { get; private set; }

        public DateTime SubscriptionStartDate { get; private set; }
        public DateTime SubscriptionEndDate { get; private set; }
        public GameServerSubscription(GameServerSubscriptionId id,
                                GameServerId gameServerId,
                                UserId buyingPlayerId,
                                string subscriptionDescription,
                                DateTime subscriptionStartDate,
                                DateTime subscriptionEndDate) : base(id)
        {
            Id = id;
            GameServerId = gameServerId;
            BuyingPlayerId = buyingPlayerId;
            SubscriptionDescription = subscriptionDescription;
            SubscriptionStartDate = subscriptionStartDate;
            SubscriptionEndDate = subscriptionEndDate;
        }
        public static GameServerSubscription Create(Guid gameServerId,
                                Guid buyingPlayerId,
                                string subscriptionDescription,
                                DateTime subscriptionStartDate,
                                DateTime subscriptionEndDate)
        {
            return new GameServerSubscription(GameServerSubscriptionId.CreateUnique(),
                                        GameServerId.Create(gameServerId),
                                        UserId.Create(buyingPlayerId),
                                        subscriptionDescription,
                                        subscriptionStartDate,
                                        subscriptionEndDate);
        }
    }
}
