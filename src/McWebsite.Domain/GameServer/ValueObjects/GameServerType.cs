using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServer.ValueObjects
{
    public sealed class GameServerType : ValueObject
    {
        public ServerType Value { get; private set; }

        private GameServerType(ServerType type)
        {
            Value = type;
        }

        public static GameServerType Create(ServerType type)
        {
            return new GameServerType(type);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
