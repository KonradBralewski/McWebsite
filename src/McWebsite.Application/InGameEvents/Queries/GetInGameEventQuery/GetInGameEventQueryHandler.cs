using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using McWebsite.Domain.InGameEvent.ValueObjects;
using MediatR;

namespace McWebsite.Application.InGameEvents.Queries.GetInGameEventQuery
{
    public sealed class GetInGameEventQueryHandler : IRequestHandler<GetInGameEventQuery, ErrorOr<GetInGameEventResult>>
    {
        private readonly IInGameEventRepository _inGameEventRepository;
        public GetInGameEventQueryHandler(IInGameEventRepository inGameEventRepository)
        {
            _inGameEventRepository = inGameEventRepository;
        }
        public async Task<ErrorOr<GetInGameEventResult>> Handle(GetInGameEventQuery query, CancellationToken cancellationToken)
        {
            var getInGameEventResult = await _inGameEventRepository.GetInGameEvent(
                InGameEventId.Create(query.InGameEventId));

            if (getInGameEventResult.IsError)
            {
                return getInGameEventResult.Errors;
            }

            return new GetInGameEventResult(getInGameEventResult.Value);
        }
    }
}
