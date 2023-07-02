using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.GameServerReport
{
    public sealed class GameServerReport : AggregateRoot<GameServerReportId>
    {
        public GameServerId GameServerId { get; }
        public UserId ReportingUserId { get; }
        public GameServerReportType ReportType { get; }
        public string ReportDescription { get; }
        public GameServerReport(GameServerReportId id,
                                GameServerId gameServerId,
                                UserId reportingUserId,
                                GameServerReportType reportType,
                                string reportDescription) : base(id)
        {
            Id = id;
            GameServerId = gameServerId;
            ReportingUserId = reportingUserId;
            ReportType = reportType;
            ReportDescription = reportDescription;
        }
        public static GameServerReport Create(Guid gameServerId,
                                              Guid reportingUserId,
                                              ReportType reportType,
                                              string reason,
                                              string description)
        {
            return new GameServerReport(GameServerReportId.CreateUnique(),
                                        GameServerId.Recreate(gameServerId),
                                        UserId.Recreate(reportingUserId),
                                        GameServerReportType.Create(reportType),
                                        description);
        }
    }
}
