using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;

namespace McWebsite.Application.GameServerSubscriptions.Queries.GetGameServersSubscriptionsQuery
{
    public sealed class GetGameServersSubscriptionsQueryHandler : IRequestHandler<GetGameServersSubscriptionsQuery, ErrorOr<GetGameServersSubscriptionsResult>>
    {
        private readonly IGameServerSubscriptionRepository _gameServerSubscriptionRepository;
        public GetGameServersSubscriptionsQueryHandler(IGameServerSubscriptionRepository gameServerSubscriptionRepository)
        {
            _gameServerSubscriptionRepository = gameServerSubscriptionRepository;
        }
        public async Task<ErrorOr<GetGameServersSubscriptionsResult>> Handle(GetGameServersSubscriptionsQuery query, CancellationToken cancellationToken)
        {
            var getGameServersSubscriptionsResult = await _gameServerSubscriptionRepository.GetGameServersSubscriptions(query.Page, query.EntriesPerPage);

            if (getGameServersSubscriptionsResult.IsError)
            {
                return getGameServersSubscriptionsResult.Errors;
            }

            return new GetGameServersSubscriptionsResult(getGameServersSubscriptionsResult.Value);
        }
    }
}
