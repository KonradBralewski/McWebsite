using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.InGameEvent.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.InGameEvent.ValueObjects
{
    public sealed class InGameEventType : ValueObject
    {
        public EventType Value { get; private set; }

        private InGameEventType(EventType type)
        {
            Value = type;
        }

        public static InGameEventType Create(EventType type)
        {
            return new InGameEventType(type);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
