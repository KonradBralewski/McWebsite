﻿using McWebsite.Domain.Common.DomainBase;

namespace McWebsite.Domain.InGameEventModel.ValueObjects
{
    public sealed class InGameEventId : ValueObject
    {
        public Guid Value { get; }

        private InGameEventId(Guid value)
        {
            Value = value;
        }

        public static InGameEventId CreateUnique()
        {
            return new InGameEventId(Guid.NewGuid());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
