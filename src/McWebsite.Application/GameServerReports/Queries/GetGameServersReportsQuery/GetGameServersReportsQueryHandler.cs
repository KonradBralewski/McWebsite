using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Queries.GetGameServersReportsQuery
{
    public sealed class GetGameServersReportsQueryHandler : IRequestHandler<GetGameServersReportsQuery, ErrorOr<GetGameServersReportsResult>>
    {
        private readonly IGameServerReportRepository _gameServerReportRepository;
        public GetGameServersReportsQueryHandler(IGameServerReportRepository gameServerReportRepository)
        {
            _gameServerReportRepository = gameServerReportRepository;
        }
        public async Task<ErrorOr<GetGameServersReportsResult>> Handle(GetGameServersReportsQuery query, CancellationToken cancellationToken)
        {
            var getGameServersReportsResult = await _gameServerReportRepository.GetGameServersReports(query.Page, query.EntriesPerPage);

            if (getGameServersReportsResult.IsError)
            {
                return getGameServersReportsResult.Errors;
            }

            return new GetGameServersReportsResult(getGameServersReportsResult.Value);
        }
    }
}
