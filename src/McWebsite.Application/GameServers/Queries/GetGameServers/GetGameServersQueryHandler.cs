using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Queries.GetGameServers
{
    internal sealed class GetGameServersQueryHandler : IRequestHandler<GetGameServersQuery, ErrorOr<IEnumerable<GameServer>>>
    {
        private readonly IGameServerRepository _gameServerRepository;
        public GetGameServersQueryHandler(IGameServerRepository gameServerRepository)
        {
            _gameServerRepository = gameServerRepository;
        }
        public Task<ErrorOr<IEnumerable<GameServer>>> Handle(GetGameServersQuery request, CancellationToken cancellationToken)
        {
            return (Task<ErrorOr<IEnumerable<GameServer>>>)Enumerable.Empty<GameServer>();
        }
    }
}
