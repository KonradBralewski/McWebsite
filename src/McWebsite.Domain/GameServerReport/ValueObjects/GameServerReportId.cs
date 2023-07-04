using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServerReport.ValueObjects
{
    public sealed class GameServerReportId : ValueObject
    {
        public Guid Value { get; private set; }

        private GameServerReportId(Guid value)
        {
            Value = value;
        }

        public static GameServerReportId CreateUnique()
        {
            return new GameServerReportId(Guid.NewGuid());
        }

        public static GameServerReportId Create(Guid id)
        {
            return new GameServerReportId(id);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
