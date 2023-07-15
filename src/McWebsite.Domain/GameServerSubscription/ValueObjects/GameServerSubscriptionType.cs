using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServerSubscription.Enums;

namespace McWebsite.Domain.GameServerSubscription.ValueObjects
{
    public sealed class GameServerSubscriptionType : ValueObject
    {
        public SubscriptionType Value { get; private set; }

        private GameServerSubscriptionType(SubscriptionType type)
        {
            Value = type;
        }

        public static GameServerSubscriptionType Create(SubscriptionType type)
        {
            return new GameServerSubscriptionType(type);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
