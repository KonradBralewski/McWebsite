using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.InGameEvent.Enums;
using McWebsite.Domain.InGameEvent.ValueObjects;
using McWebsite.Domain.User.ValueObjects;

namespace McWebsite.Domain.InGameEvent.Entities
{
    public sealed class InGameEvent : Entity<InGameEventId>
    {
        public GameServerId GameServerId { get; private set; }
        public int InGameId { get; private set; }
        public InGameEventType InGameEventType { get; private set; }
        public string Description { get; private set; }
        public float Price { get; private set; }

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private InGameEvent(InGameEventId id,
                            GameServerId gameServerId,
                            int inGameId,
                            InGameEventType eventType,
                            string description,
                            float price,
                            DateTime createdDateTime,
                            DateTime updatedDateTime) : base(id)
        {
            GameServerId = gameServerId;
            InGameId = inGameId;
            InGameEventType = eventType;
            Description = description;
            Price = price;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static InGameEvent Create(Guid gameServerId,
                                         int inGameId,
                                         EventType eventType,
                                         string description,
                                         float price,
                                         DateTime createdDateTime,
                                         DateTime updatedDateTime)
        {
            return new InGameEvent(InGameEventId.CreateUnique(),
                                   GameServerId.Create(gameServerId),
                                   inGameId,
                                   InGameEventType.Create(eventType),
                                   description,
                                   price,
                                   createdDateTime,
                                   updatedDateTime);
        }

        public static InGameEvent Recreate(Guid id,
                                           Guid gameServerId,
                                           int inGameId,
                                           EventType eventType,
                                           string description,
                                           float price,
                                           DateTime createdDateTime,
                                           DateTime updatedDateTime)
        {
            return new InGameEvent(InGameEventId.Create(id),
                                   GameServerId.Create(gameServerId),
                                   inGameId,
                                   InGameEventType.Create(eventType),
                                   description,
                                   price,
                                   createdDateTime,
                                   updatedDateTime);
        }

        /// <summary>
        /// Constructor that will be used by EF Core, EF Core is not able to setup navigation property for Tuple<UserId, UserId>
        /// </summary>
#pragma warning disable CS8618
        private InGameEvent(InGameEventId id) : base(id)
        {

        }

#pragma warning restore CS8618
    }
}
