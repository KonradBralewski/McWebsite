using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.Common.Utilities;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Commands.CreateGameServerCommand
{
    internal sealed class CreateGameServerCommandHandler : IRequestHandler<CreateGameServerCommand, ErrorOr<CreateGameServerResult>>
    {
        private readonly IGameServerRepository _gameServerRepository;
        public CreateGameServerCommandHandler(IGameServerRepository gameServerRepository)
        {
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<CreateGameServerResult>> Handle(CreateGameServerCommand request, CancellationToken cancellationToken)
        {
            GameServer toBeAdded = GameServer.Create(request.MaximumPlayersNumber,
                                                     0,
                                                     request.ServerLocation.ToEnum<ServerLocation>(),
                                                     request.ServerType.ToEnum<ServerType>(),
                                                     request.Description,
                                                     DateTime.UtcNow,
                                                     DateTime.UtcNow);

           var creationResult = await _gameServerRepository.CreateGameServer(toBeAdded);

            if (creationResult.IsError)
            {
                return creationResult.Errors;
            }

            GameServer createdGameServer = creationResult.Value;

            return new CreateGameServerResult(createdGameServer);
        }
    }
}
