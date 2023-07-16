using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;

namespace McWebsite.Application.InGameEvents.Queries.GetInGameEventsQuery
{
    public sealed class GetInGameEventsQueryHandler : IRequestHandler<GetInGameEventsQuery, ErrorOr<GetInGameEventsResult>>
    {
        private readonly IInGameEventRepository _inGameEventRepository;
        public GetInGameEventsQueryHandler(IInGameEventRepository inGameEventRepository)
        {
            _inGameEventRepository = inGameEventRepository;
        }
        public async Task<ErrorOr<GetInGameEventsResult>> Handle(GetInGameEventsQuery query, CancellationToken cancellationToken)
        {
            var getInGameEventsResult = await _inGameEventRepository.GetInGameEvents(query.Page, query.EntriesPerPage);

            if (getInGameEventsResult.IsError)
            {
                return getInGameEventsResult.Errors;
            }

            return new GetInGameEventsResult(getInGameEventsResult.Value);
        }
    }
}
