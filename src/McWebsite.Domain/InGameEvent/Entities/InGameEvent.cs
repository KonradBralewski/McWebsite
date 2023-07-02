using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.InGameEvent.Enums;
using McWebsite.Domain.InGameEvent.ValueObjects;
using McWebsite.Domain.InGameEventModel.ValueObjects;
using McWebsite.Domain.User.ValueObjects;

namespace McWebsite.Domain.InGameEventModel.Entities
{
    public sealed class InGameEvent : Entity<InGameEventId>
    {
        public GameServerId GameServerId { get; }
        public int InGameId { get; }
        public InGameEventType InGameEventType { get; }
        public string Description { get; }
        public float Price { get; }

        public DateTime UpdatedDateTime { get; }

        private InGameEvent(InGameEventId id,
                            GameServerId gameServerId,
                            int inGameId,
                            InGameEventType eventType,
                            string description,
                            float price) : base(id)
        {
            GameServerId = gameServerId;
            InGameId = inGameId;
            InGameEventType = eventType;
            Description = description;
            Price = price;
        }

        public static InGameEvent Create(Guid gameServerId,
                                         int inGameId,
                                         EventType eventType,
                                         string description,
                                         float price)
        {
            return new InGameEvent(InGameEventId.CreateUnique(),
                                   GameServerId.Recreate(gameServerId),
                                   inGameId,
                                   InGameEventType.Create(eventType),
                                   description,
                                   price);
        }
    }
}
