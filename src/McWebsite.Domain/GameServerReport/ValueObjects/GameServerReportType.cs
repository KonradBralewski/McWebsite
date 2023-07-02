using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerReport.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServerReport.ValueObjects
{
    public sealed class GameServerReportType : ValueObject
    {
        public ReportType Value { get; }

        private GameServerReportType(ReportType type)
        {
            Value = type;
        }

        public static GameServerReportType Create(ReportType type)
        {
            return new GameServerReportType(type);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
