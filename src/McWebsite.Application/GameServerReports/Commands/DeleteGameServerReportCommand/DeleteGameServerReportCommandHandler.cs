using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.ValueObjects;
using MediatR;

namespace McWebsite.Application.GameServerReports.Commands.DeleteGameServerReportCommand
{
    public sealed class DeleteGameServerReportCommandHandler : IRequestHandler<DeleteGameServerReportCommand, ErrorOr<bool>>
    {
        private readonly IGameServerReportRepository _gameServerReportRepository;
        public DeleteGameServerReportCommandHandler(IGameServerReportRepository gameServerReportRepository)
        {
            _gameServerReportRepository = gameServerReportRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteGameServerReportCommand request, CancellationToken cancellationToken)
        {
            var gameServerSearchResult = await _gameServerReportRepository.GetGameServerReport(GameServerReportId.Create(request.GameServerReportId));

            if (gameServerSearchResult.IsError)
            {
                return gameServerSearchResult.Errors;
            }

            GameServerReport gameServerReport = gameServerSearchResult.Value;

            await _gameServerReportRepository.DeleteGameServerReport(gameServerReport);

            return true;
        }
    }
}
