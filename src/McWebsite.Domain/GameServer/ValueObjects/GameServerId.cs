using McWebsite.Domain.Common.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServer.ValueObjects
{
    public sealed class GameServerId : ValueObject
    {
        public Guid Value { get; }

        private GameServerId(Guid value)
        {
            Value = value;
        }

        public static GameServerId CreateUnique()
        {
            return new GameServerId(Guid.NewGuid());
        }

        public static GameServerId Recreate(Guid id)
        {
            return new GameServerId(id);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
