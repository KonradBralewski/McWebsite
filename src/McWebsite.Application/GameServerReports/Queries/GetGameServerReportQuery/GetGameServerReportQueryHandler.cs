using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.GameServers.Queries.GetGameServer;
using McWebsite.Application.GameServers.Queries.GetGameServerQuery;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerReport.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Queries.GetGameServerReportQuery
{
    internal sealed class GetGameServerReportQueryHandler : IRequestHandler<GetGameServerReportQuery, ErrorOr<GetGameServerReportResult>>
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
