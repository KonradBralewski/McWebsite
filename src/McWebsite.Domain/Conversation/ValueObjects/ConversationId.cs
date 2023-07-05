using McWebsite.Domain.Common.DomainBase;

namespace McWebsite.Domain.Conversation.ValueObjects
{
    public sealed class ConversationId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ConversationId(Guid value)
        {
            Value = value;
        }

        public static ConversationId CreateUnique()
        {
            return new ConversationId(Guid.NewGuid());
        }

        public static ConversationId Create(Guid id)
        {
            return new ConversationId(id);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
