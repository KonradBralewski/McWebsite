using FluentAssertions;
using McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand;
using McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Commands.UpdateGameServerCommand;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.Enums;

namespace McWebsite.Application.UnitTests.TestUtils.Extensions.GameServerReportExtensions
{
    public static class GameServerReportValidationExtensions
    {
        public static void ValidateIfCreatedFrom(this GameServerReport gameServerReport, CreateGameServerReportCommand command)
        {
            gameServerReport.Id.Value.ToString().Should().NotBeEmpty();

            gameServerReport.ReportingUserId.Value.Should().Be(command.ReportingUserId);

            gameServerReport.GameServerId.Value.Should().Be(command.ReportedGameServerId);

            gameServerReport.ReportType.Value.ToString().Should().Be(command.ReportType);

            gameServerReport.ReportDescription.Should().Be(command.ReportDescription);
        }

        public static void ValidateIfUpdatedFrom(this GameServerReport gameServerReport, UpdateGameServerReportCommand command)
        {
            gameServerReport.Id.Value.Should().Be(command.GameServerReportId);

            gameServerReport.GameServerId.Value.Should().Be(command.ReportedGameServerId);

            gameServerReport.ReportType.Value.ToString().Should().Be(command.ReportType);

            gameServerReport.ReportDescription.Should().Be(command.ReportDescription);
        }
    }
}
