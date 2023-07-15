using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using MediatR;

namespace McWebsite.Application.GameServerSubscriptions.Queries.GetGameServerSubscriptionQuery
{
    public sealed class GetGameServerSubscriptionQueryHandler : IRequestHandler<GetGameServerSubscriptionQuery, ErrorOr<GetGameServerSubscriptionResult>>
    {
        private readonly IGameServerSubscriptionRepository _gameServerSubscriptionRepository;
        public GetGameServerSubscriptionQueryHandler(IGameServerSubscriptionRepository gameServerSubscriptionRepository)
        {
            _gameServerSubscriptionRepository = gameServerSubscriptionRepository;
        }
        public async Task<ErrorOr<GetGameServerSubscriptionResult>> Handle(GetGameServerSubscriptionQuery query, CancellationToken cancellationToken)
        {
            var getGameServerSubscriptionResult = await _gameServerSubscriptionRepository.GetGameServerSubscription(
                GameServerSubscriptionId.Create(query.GameServerSubscriptionId));

            if (getGameServerSubscriptionResult.IsError)
            {
                return getGameServerSubscriptionResult.Errors;
            }

            return new GetGameServerSubscriptionResult(getGameServerSubscriptionResult.Value);
        }
    }
}
