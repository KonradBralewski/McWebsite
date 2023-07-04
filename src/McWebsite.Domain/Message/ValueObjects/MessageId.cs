using McWebsite.Domain.Common.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.MessageModel.ValueObjects
{
    public sealed class MessageId : ValueObject
    {
        public Guid Value { get; private set; }

        private MessageId(Guid value)
        {
            Value = value;
        }

        public static MessageId CreateUnique()
        {
            return new MessageId(Guid.NewGuid());
        }

        public static MessageId Create(Guid value)
        {
            return new MessageId(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
