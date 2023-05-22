using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.InGameEventModel.ValueObjects;

namespace McWebsite.Domain.InGameEventModel.Entities
{
    public sealed class InGameEvent : Entity<InGameEventId>
    {
        public GameServerId GameServerId { get; }
        public string Description { get; }
        public float Price { get; }

        private InGameEvent(InGameEventId id, string description, float price, GameServerId gameServerId) : base(id)
        {
            Price = price;
            Description = description;
            GameServerId = gameServerId;
        }

        public static InGameEvent Create(string description, float price, Guid gameServerId)
        {
            return new InGameEvent(InGameEventId.CreateUnique(), description, price, GameServerId.Recreate(gameServerId));
        }
    }
}
