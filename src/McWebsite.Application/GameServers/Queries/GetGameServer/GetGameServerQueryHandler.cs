using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Queries.GetGameServer
{
    internal sealed class GetGameServerQueryHandler : IRequestHandler<GetGameServerQuery, ErrorOr<GameServer>>
    {
        private readonly IGameServerRepository _gameServerRepository;
        public GetGameServerQueryHandler(IGameServerRepository gameServerRepository)
        {
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<GameServer>> Handle(GetGameServerQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            GameServer gameServer = await _gameServerRepository.GetGameServer(GameServerId.Create(query.GameServerId));

            if(gameServer is null)
            {
                return Errors.DomainModels.ModelNotFound;
            }

            return gameServer;
        }
    }
}
