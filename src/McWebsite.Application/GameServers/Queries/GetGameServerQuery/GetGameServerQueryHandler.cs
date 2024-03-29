﻿using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.GameServers.Queries.GetGameServerQuery;
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
    public sealed class GetGameServerQueryHandler : IRequestHandler<GetGameServerQuery, ErrorOr<GetGameServerResult>>
    {
        private readonly IGameServerRepository _gameServerRepository;
        public GetGameServerQueryHandler(IGameServerRepository gameServerRepository)
        {
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<GetGameServerResult>> Handle(GetGameServerQuery query, CancellationToken cancellationToken)
        {
            var getGameServerResult = await _gameServerRepository.GetGameServer(GameServerId.Create(query.GameServerId));

            if(getGameServerResult.IsError)
            {
                return getGameServerResult.Errors;
            }

            return new GetGameServerResult(getGameServerResult.Value);
        }
    }
}
