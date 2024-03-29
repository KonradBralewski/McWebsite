﻿using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Commands.DeleteGameServerCommand
{
    public sealed class DeleteGameServerCommandHandler : IRequestHandler<DeleteGameServerCommand, ErrorOr<bool>>
    {
        private readonly IGameServerRepository _gameServerRepository;
        public DeleteGameServerCommandHandler(IGameServerRepository gameServerRepository)
        {
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteGameServerCommand request, CancellationToken cancellationToken)
        {
            var gameServerSearchResult = await _gameServerRepository.GetGameServer(GameServerId.Create(request.GameServerId));

            if(gameServerSearchResult.IsError)
            {
                return gameServerSearchResult.Errors;
            }

            GameServer gameServer = gameServerSearchResult.Value;

            gameServer.Delete();

            await _gameServerRepository.DeleteGameServer(gameServer);

            return true;
        }
    }
}
