using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServerReport.ValueObjects;
using MediatR;

namespace McWebsite.Application.GameServerReports.Queries.GetGameServerReportQuery
{
    public sealed class GetGameServerReportQueryHandler : IRequestHandler<GetGameServerReportQuery, ErrorOr<GetGameServerReportResult>>
    {
        private readonly IGameServerReportRepository _gameServerReportRepository;
        public GetGameServerReportQueryHandler(IGameServerReportRepository gameServerReportRepository)
        {
            _gameServerReportRepository = gameServerReportRepository;
        }
        public async Task<ErrorOr<GetGameServerReportResult>> Handle(GetGameServerReportQuery query, CancellationToken cancellationToken)
        {
            var getGameServerReportResult = await _gameServerReportRepository.GetGameServerReport(GameServerReportId.Create(query.GameServerReportId));

            if (getGameServerReportResult.IsError)
            {
                return getGameServerReportResult.Errors;
            }

            return new GetGameServerReportResult(getGameServerReportResult.Value);
        }
    }
}
