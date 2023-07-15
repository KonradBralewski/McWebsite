using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.Common.Utilities;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Domain.GameServerSubscription.Enums;
using MediatR;

namespace McWebsite.Application.GameServerSubscriptions.Commands.CreateGameServerSubscriptionCommand
{
    public sealed class CreateGameServerSubscriptionCommandHandler : IRequestHandler<CreateGameServerSubscriptionCommand, ErrorOr<CreateGameServerSubscriptionResult>>
    {
        private readonly IGameServerSubscriptionRepository _gameServerSubscriptionRepository;
        private readonly IGameServerRepository _gameServerRepository;
        public CreateGameServerSubscriptionCommandHandler(IGameServerSubscriptionRepository gameServerSubscriptionRepository, IGameServerRepository gameServerRepository)
        {
            _gameServerSubscriptionRepository = gameServerSubscriptionRepository;
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<CreateGameServerSubscriptionResult>> Handle(CreateGameServerSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var gameServerSearchResult = await _gameServerRepository.GetGameServer(GameServerId.Create(command.GameServerId));

            if (gameServerSearchResult.IsError)
            {
                return gameServerSearchResult.Errors;
            }

            GameServerSubscription toBeAdded = GameServerSubscription.Create(command.GameServerId,
                                                     command.SubscriptionType.ToEnum<SubscriptionType>(),
                                                     command.InGameSubscriptionId,
                                                     command.Price,
                                                     command.SubscriptionDescription,
                                                     command.SubscriptionDuration,
                                                     DateTime.UtcNow,
                                                     DateTime.UtcNow);

            var creationResult = await _gameServerSubscriptionRepository.CreateGameServerSubscription(toBeAdded);

            if (creationResult.IsError)
            {
                return creationResult.Errors;
            }

            GameServerSubscription createdGameServerSubscription = creationResult.Value;

            return new CreateGameServerSubscriptionResult(createdGameServerSubscription);
        }
    }
}
