using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using MediatR;

namespace McWebsite.Application.GameServerSubscriptions.Commands.DeleteGameServerSubscriptionCommand
{
    public sealed class DeleteGameServerSubscriptionCommandHandler : IRequestHandler<DeleteGameServerSubscriptionCommand, ErrorOr<bool>>
    {
        private readonly IGameServerSubscriptionRepository _gameServerSubscriptionRepository;
        public DeleteGameServerSubscriptionCommandHandler(IGameServerSubscriptionRepository gameServerSubscriptionRepository)
        {
            _gameServerSubscriptionRepository = gameServerSubscriptionRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteGameServerSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var gameServerSubscriptionSearchResult = await _gameServerSubscriptionRepository.GetGameServerSubscription(
                GameServerSubscriptionId.Create(request.GameServerSubscriptionId));

            if (gameServerSubscriptionSearchResult.IsError)
            {
                return gameServerSubscriptionSearchResult.Errors;
            }

            GameServerSubscription gameServerSubscription = gameServerSubscriptionSearchResult.Value;

            await _gameServerSubscriptionRepository.DeleteGameServerSubscription(gameServerSubscription);

            return true;
        }
    }
}
