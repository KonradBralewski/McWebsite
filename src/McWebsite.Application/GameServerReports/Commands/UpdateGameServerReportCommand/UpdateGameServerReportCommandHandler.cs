using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;
using McWebsite.Application.Common.Utilities;
using McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand;
using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServer.ValueObjects;

namespace McWebsite.Application.GameServers.Commands.UpdateGameServerCommand
{
    public sealed class UpdateGameServerReportCommandHandler : IRequestHandler<UpdateGameServerReportCommand, ErrorOr<UpdateGameServerReportResult>?>
    {
        private readonly IGameServerReportRepository _gameServerReportRepository;
        private readonly IGameServerRepository _gameServerRepository;
        public UpdateGameServerReportCommandHandler(IGameServerReportRepository gameServerReportRepository, IGameServerRepository gameServerRepository)
        {
            _gameServerReportRepository = gameServerReportRepository;
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<UpdateGameServerReportResult>?> Handle(UpdateGameServerReportCommand command, CancellationToken cancellationToken)
        {
            var gameServerReportSearchResult = await _gameServerReportRepository.GetGameServerReport(GameServerReportId.Create(command.GameServerReportId));

            if (gameServerReportSearchResult.IsError)
            {
                return gameServerReportSearchResult.Errors;
            }

            var gameServerSearchResult = await _gameServerRepository.GetGameServer(GameServerId.Create(command.ReportedGameServerId));

            if (gameServerSearchResult.IsError)
            {
                return gameServerSearchResult.Errors;
            }

            GameServerReport foundGameServerReport = gameServerReportSearchResult.Value;

            if (ApplyModfications(foundGameServerReport, command) is not GameServerReport gameServerAfterUpdate)
            {
                return null;
            }

            var updatedGameServerReportResult = await _gameServerReportRepository.UpdateGameServerReport(gameServerAfterUpdate);

            if (updatedGameServerReportResult.IsError)
            {
                return updatedGameServerReportResult.Errors;
            }

            GameServerReport updatedGameServerReport = updatedGameServerReportResult.Value;


            return new UpdateGameServerReportResult(updatedGameServerReport);
        }

        private GameServerReport? ApplyModfications(GameServerReport toBeUpdated, UpdateGameServerReportCommand command)
        {
            bool hasSomethingChanged = false;

            if (toBeUpdated.GameServerId.Value != command.ReportedGameServerId
                || toBeUpdated.ReportType.Value.ToString() != command.ReportType
                || toBeUpdated.ReportDescription != command.ReportDescription)
            {
                hasSomethingChanged = true;
            }

            if (!hasSomethingChanged)
            {
                return null;
            }

            return GameServerReport.Recreate(toBeUpdated.Id.Value,
                                       command.ReportedGameServerId,
                                       toBeUpdated.ReportingUserId.Value,
                                       command.ReportType.ToEnum<ReportType>(),
                                       command.ReportDescription,
                                       toBeUpdated.ReportDate,
                                       DateTime.UtcNow);
        }
    }
}
