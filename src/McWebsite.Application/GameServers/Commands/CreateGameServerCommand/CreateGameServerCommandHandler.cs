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
    public sealed class CreateGameServerCommandHandler : IRequestHandler<CreateGameServerCommand, ErrorOr<CreateGameServerResult>>
    {
        private readonly IGameServerRepository _gameServerRepository;
        public CreateGameServerCommandHandler(IGameServerRepository gameServerRepository)
        {
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<CreateGameServerResult>> Handle(CreateGameServerCommand command, CancellationToken cancellationToken)
        {
            GameServer toBeAdded = GameServer.Create(command.MaximumPlayersNumber,
                                                     0,
                                                     command.ServerLocation.ToEnum<ServerLocation>(),
                                                     command.ServerType.ToEnum<ServerType>(),
                                                     command.Description,
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
