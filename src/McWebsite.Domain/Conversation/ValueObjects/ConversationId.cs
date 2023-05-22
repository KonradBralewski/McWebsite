using McWebsite.Domain.Common.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Conversation.ValueObjects
{
    public sealed class ConversationId : ValueObject
    {
        public Guid Value { get; }

        private ConversationId(Guid value)
        {
            Value = value;
        }

        public static ConversationId CreateUnique()
        {
            return new ConversationId(Guid.NewGuid());
        }

        public static ConversationId Recreate(Guid id)
        {
            return new ConversationId(id);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
