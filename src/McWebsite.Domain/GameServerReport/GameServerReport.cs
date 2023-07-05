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
    public sealed class GameServerReport : AggregateRoot<GameServerReportId, Guid>
    {
        public GameServerId GameServerId { get; private set; }
        public UserId ReportingUserId { get; private set; }
        public GameServerReportType ReportType { get; private set; }
        public string ReportDescription { get; private set; }
        public DateTime ReportDate { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }
        public GameServerReport(GameServerReportId id,
                                GameServerId gameServerId,
                                UserId reportingUserId,
                                GameServerReportType reportType,
                                string reportDescription,
                                DateTime reportDate,
                                DateTime updatedDateTime) : base(id)
        {
            Id = id;
            GameServerId = gameServerId;
            ReportingUserId = reportingUserId;
            ReportType = reportType;
            ReportDescription = reportDescription;
            ReportDate = reportDate;
            UpdatedDateTime = updatedDateTime;
        }
        public static GameServerReport Create(Guid gameServerId,
                                              Guid reportingUserId,
                                              ReportType reportType,
                                              string reason,
                                              string description,
                                              DateTime reportDate,
                                              DateTime updatedDateTime)
        {
            return new GameServerReport(GameServerReportId.CreateUnique(),
                                        GameServerId.Create(gameServerId),
                                        UserId.Create(reportingUserId),
                                        GameServerReportType.Create(reportType),
                                        description, 
                                        reportDate, 
                                        updatedDateTime);
        }


        /// <summary>
        /// Constructor that will be used by EF Core, EF Core is not able to setup navigation property for Tuple<UserId, UserId>
        /// </summary>
#pragma warning disable CS8618
        private GameServerReport()
        {

        }

#pragma warning restore CS8618

    }
}
