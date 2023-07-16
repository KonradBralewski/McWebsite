using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;
using McWebsite.Application.Common.Utilities;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerSubscription.Enums;
using McWebsite.Domain.InGameEvent.ValueObjects;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEvent.Enums;

namespace McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand
{
    public sealed class UpdateInGameEventCommandHandler : IRequestHandler<UpdateInGameEventCommand, ErrorOr<UpdateInGameEventResult>?>
    {
        private readonly IInGameEventRepository _inGameEventRepository;
        private readonly IGameServerRepository _gameServerRepository;
        public UpdateInGameEventCommandHandler(IInGameEventRepository inGameEventRepository, IGameServerRepository gameServerRepository)
        {
            _inGameEventRepository = inGameEventRepository;
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<UpdateInGameEventResult>?> Handle(UpdateInGameEventCommand command, CancellationToken cancellationToken)
        {
            var inGameEventSearchResult = await _inGameEventRepository.GetInGameEvent(
                InGameEventId.Create(command.InGameEventId));

            if (inGameEventSearchResult.IsError)
            {
                return inGameEventSearchResult.Errors;
            }

            var gameServerSearchResult = await _gameServerRepository.GetGameServer(GameServerId.Create(command.GameServerId));

            if (gameServerSearchResult.IsError)
            {
                return gameServerSearchResult.Errors;
            }

            InGameEvent foundInGameEvent = inGameEventSearchResult.Value;

            if (ApplyModfications(foundInGameEvent, command) is not InGameEvent inGameEventAfterUpdate)
            {
                return null;
            }

            var updatedInGameEventResult = await _inGameEventRepository.UpdateInGameEvent(inGameEventAfterUpdate);

            if (updatedInGameEventResult.IsError)
            {
                return updatedInGameEventResult.Errors;
            }

            InGameEvent updatedInGameEvent = updatedInGameEventResult.Value;


            return new UpdateInGameEventResult(updatedInGameEvent);
        }

        private InGameEvent? ApplyModfications(InGameEvent toBeUpdated, UpdateInGameEventCommand command)
        {
            bool hasSomethingChanged = false;

            if (toBeUpdated.GameServerId.Value != command.GameServerId
                || toBeUpdated.InGameId != command.InGameId
                || toBeUpdated.InGameEventType.Value.ToString() != command.InGameEventType
                || toBeUpdated.Description != command.Description
                || toBeUpdated.Price != command.Price)
            {
                hasSomethingChanged = true;
            }

            if (!hasSomethingChanged)
            {
                return null;
            }

            return InGameEvent.Recreate(toBeUpdated.Id.Value,
                                       command.GameServerId,
                                       command.InGameId,
                                       command.InGameEventType.ToEnum<EventType>(),
                                       command.Description,
                                       command.Price,
                                       toBeUpdated.CreatedDateTime,
                                       DateTime.UtcNow);
        }
    }
}
