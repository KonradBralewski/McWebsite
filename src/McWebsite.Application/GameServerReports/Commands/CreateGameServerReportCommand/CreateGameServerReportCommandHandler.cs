using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.Common.Utilities;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand
{
    public sealed class CreateGameServerReportCommandHandler : IRequestHandler<CreateGameServerReportCommand, ErrorOr<CreateGameServerReportResult>>
    {
        private readonly IGameServerReportRepository _gameServerReportRepository;
        private readonly IGameServerRepository _gameServerRepository;
        public CreateGameServerReportCommandHandler(IGameServerReportRepository gameServerReportRepository, IGameServerRepository gameServerRepository)
        {
            _gameServerReportRepository = gameServerReportRepository;
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<CreateGameServerReportResult>> Handle(CreateGameServerReportCommand command, CancellationToken cancellationToken)
        {
            var gameServerSearchResult = await _gameServerRepository.GetGameServer(GameServerId.Create(command.ReportedGameServerId));

            if (gameServerSearchResult.IsError)
            {
                return gameServerSearchResult.Errors;
            }

            GameServerReport toBeAdded = GameServerReport.Create(command.ReportedGameServerId,
                                                     command.ReportingUserId,
                                                     command.ReportType.ToEnum<ReportType>(),
                                                     command.ReportDescription,
                                                     DateTime.UtcNow,
                                                     DateTime.UtcNow);

            var creationResult = await _gameServerReportRepository.CreateGameServerReport(toBeAdded);

            if (creationResult.IsError)
            {
                return creationResult.Errors;
            }

            GameServerReport createdGameServerReport = creationResult.Value;

            return new CreateGameServerReportResult(createdGameServerReport);
        }
    }
}
