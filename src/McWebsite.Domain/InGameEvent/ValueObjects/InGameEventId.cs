using McWebsite.Domain.Common.DomainBase;

namespace McWebsite.Domain.InGameEvent.ValueObjects
{
    public sealed class InGameEventId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private InGameEventId(Guid value)
        {
            Value = value;
        }

        public static InGameEventId CreateUnique()
        {
            return new InGameEventId(Guid.NewGuid());
        }

        public static InGameEventId Create(Guid value)
        {
            return new InGameEventId(value);
        }



        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
