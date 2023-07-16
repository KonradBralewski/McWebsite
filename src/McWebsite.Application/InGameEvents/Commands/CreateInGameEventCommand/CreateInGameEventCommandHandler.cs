using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.Common.Utilities;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEvent.Enums;
using MediatR;

namespace McWebsite.Application.InGameEvents.Commands.CreateInGameEventCommand
{
    public sealed class CreateInGameEventCommandHandler : IRequestHandler<CreateInGameEventCommand, ErrorOr<CreateInGameEventResult>>
    {
        private readonly IInGameEventRepository _inGameEventRepository;
        private readonly IGameServerRepository _gameServerRepository;
        public CreateInGameEventCommandHandler(IInGameEventRepository inGameEventRepository, IGameServerRepository gameServerRepository)
        {
            _inGameEventRepository = inGameEventRepository;
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<CreateInGameEventResult>> Handle(CreateInGameEventCommand command, CancellationToken cancellationToken)
        {
            var gameServerSearchResult = await _gameServerRepository.GetGameServer(GameServerId.Create(command.GameServerId));

            if (gameServerSearchResult.IsError)
            {
                return gameServerSearchResult.Errors;
            }

            InGameEvent toBeAdded = InGameEvent.Create(command.GameServerId,
                                                     command.InGameId,
                                                     command.InGameEventType.ToEnum<EventType>(),
                                                     command.Description,
                                                     command.Price,
                                                     DateTime.UtcNow,
                                                     DateTime.UtcNow);

            var creationResult = await _inGameEventRepository.CreateInGameEvent(toBeAdded);

            if (creationResult.IsError)
            {
                return creationResult.Errors;
            }

            InGameEvent createdInGameEvent = creationResult.Value;

            return new CreateInGameEventResult(createdInGameEvent);
        }
    }
}
