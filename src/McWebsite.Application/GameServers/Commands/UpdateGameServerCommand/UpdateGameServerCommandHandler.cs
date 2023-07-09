using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServer;
using MediatR;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.Common.Utilities;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.Common.Errors;

namespace McWebsite.Application.GameServers.Commands.UpdateGameServerCommand
{
    internal class UpdateGameServerCommandHandler : IRequestHandler<UpdateGameServerCommand, ErrorOr<UpdateGameServerResult>?>
    {
        private readonly IGameServerRepository _gameServerRepository;
        public UpdateGameServerCommandHandler(IGameServerRepository gameServerRepository)
        {
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<UpdateGameServerResult>?> Handle(UpdateGameServerCommand command, CancellationToken cancellationToken)
        {
            var gameServerSearchResult = await _gameServerRepository.GetGameServer(GameServerId.Create(command.GameServerId));

            if (gameServerSearchResult.IsError)
            {
                return gameServerSearchResult.Errors;
            }

            GameServer foundGameServer = gameServerSearchResult.Value;

            foundGameServer.Update();

            if(ApplyModfications(foundGameServer, command) is not GameServer gameServerAfterUpdate)
            {
                return null;
            }

            var updatedGameServerResult = await _gameServerRepository.UpdateGameServer(gameServerAfterUpdate);

            if (updatedGameServerResult.IsError)
            {
                return updatedGameServerResult.Errors;
            }

            GameServer updatedGameServer = gameServerSearchResult.Value;


            return new UpdateGameServerResult(updatedGameServer.Id.Value,
                                              updatedGameServer.MaximumPlayersNumber,
                                              updatedGameServer.CurrentPlayersNumber,
                                              updatedGameServer.ServerLocation.Value.ToString(),
                                              updatedGameServer.ServerType.Value.ToString(),
                                              updatedGameServer.Description);
        }

        private GameServer? ApplyModfications(GameServer toBeUpdated, UpdateGameServerCommand command)
        {
            bool hasSomethingChanged = false;

            if(toBeUpdated.CurrentPlayersNumber != command.CurrentPlayersNumber
                || toBeUpdated.MaximumPlayersNumber != command.MaximumPlayersNumber
                || toBeUpdated.ServerLocation.Value.ToString() != command.ServerLocation
                || toBeUpdated.ServerType.Value.ToString() != command.ServerType
                || toBeUpdated.Description != command.Description)
            {
                hasSomethingChanged = true;
            }

            if (!hasSomethingChanged)
            {
                return null;
            }

            return GameServer.Recreate(command.GameServerId,
                                       command.MaximumPlayersNumber,
                                       command.CurrentPlayersNumber,
                                       command.ServerLocation.ToEnum<ServerLocation>(),
                                       command.ServerType.ToEnum<ServerType>(),
                                       command.Description,
                                       toBeUpdated.CreatedDateTime,
                                       DateTime.UtcNow);
        }
    }
}
