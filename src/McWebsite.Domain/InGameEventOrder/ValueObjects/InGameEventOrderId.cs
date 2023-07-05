using McWebsite.Domain.Common.DomainBase;

namespace McWebsite.Domain.InGameEventOrder.ValueObjects
{
    public sealed class InGameEventOrderId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private InGameEventOrderId(Guid value)
        {
            Value = value;
        }

        public static InGameEventOrderId CreateUnique()
        {
            return new InGameEventOrderId(Guid.NewGuid());
        }

        public static InGameEventOrderId Create(Guid id)
        {
            return new InGameEventOrderId(id);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
