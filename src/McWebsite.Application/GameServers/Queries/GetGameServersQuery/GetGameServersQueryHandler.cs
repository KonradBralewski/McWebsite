using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.GameServer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Queries.GetGameServers
{
    public sealed class GetGameServersQueryHandler : IRequestHandler<GetGameServersQuery, ErrorOr<GetGameServersResult>>
    {
        private readonly IGameServerRepository _gameServerRepository;
        public GetGameServersQueryHandler(IGameServerRepository gameServerRepository)
        {
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<GetGameServersResult>> Handle(GetGameServersQuery query, CancellationToken cancellationToken)
        {
            var getGameServersResult = await _gameServerRepository.GetGameServers(query.Page, query.EntriesPerPage);

            if (getGameServersResult.IsError)
            {
                return getGameServersResult.Errors;
            }

            return new GetGameServersResult(getGameServersResult.Value);
        }
    }
}
