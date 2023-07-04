using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServerReport.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServerSubscription.ValueObjects
{
    public sealed class GameServerSubscriptionId : ValueObject
    {
        public Guid Value { get; private set; }

        private GameServerSubscriptionId(Guid value)
        {
            Value = value;
        }

        public static GameServerSubscriptionId CreateUnique()
        {
            return new GameServerSubscriptionId(Guid.NewGuid());
        }

        public static GameServerSubscriptionId Create(Guid id)
        {
            return new GameServerSubscriptionId(id);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
