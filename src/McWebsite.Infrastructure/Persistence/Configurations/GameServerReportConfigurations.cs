using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Persistence.Configurations
{
    public sealed class GameServerReportConfigurations : IEntityTypeConfiguration<GameServerReport>
    {
        public void Configure(EntityTypeBuilder<GameServerReport> builder)
        {
            builder.ToTable("GameServerReports");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => GameServerReportId.Create(value));

            builder.Property(x => x.GameServerId)
                .ValueGeneratedNever()
                .IsRequired()
                .HasConversion(
                gameServerId => gameServerId.Value,
                value => GameServerId.Create(value));

            builder.Property(x => x.ReportingUserId)
                .ValueGeneratedNever()
                .IsRequired()
                .HasConversion(
                reportingUserId => reportingUserId.Value,
                value => UserId.Create(value));

            builder.Property(x => x.ReportType)
                .IsRequired()
                .HasConversion(
                reportType => reportType.Value,
                value => GameServerReportType.Create(value));

            builder.Property(x => x.ReportDescription)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
