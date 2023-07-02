﻿using McWebsite.Domain.Common.DomainBase;

namespace McWebsite.Domain.InGameEventOrder.ValueObjects
{
    public sealed class InGameEventOrderId : ValueObject
    {
        public Guid Value { get; }

        private InGameEventOrderId(Guid value)
        {
            Value = value;
        }

        public static InGameEventOrderId CreateUnique()
        {
            return new InGameEventOrderId(Guid.NewGuid());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
