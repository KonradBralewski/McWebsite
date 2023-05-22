using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServer.ValueObjects
{
    public sealed class GameServerLocation : ValueObject
    {
        public ServerLocation Value { get; }

        private GameServerLocation(ServerLocation location)
        {
            Value = location;
        }

        public static GameServerLocation Create(ServerLocation location)
        {
            return new GameServerLocation(location);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
