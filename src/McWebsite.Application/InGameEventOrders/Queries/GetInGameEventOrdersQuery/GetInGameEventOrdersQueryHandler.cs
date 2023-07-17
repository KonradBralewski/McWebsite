using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrdersQuery
{
    public sealed class GetInGameEventOrdersQueryHandler : IRequestHandler<GetInGameEventOrdersQuery, ErrorOr<GetInGameEventOrdersResult>>
    {
        private readonly IInGameEventOrderRepository _inGameEventOrderRepository;
        public GetInGameEventOrdersQueryHandler(IInGameEventOrderRepository inGameEventOrderRepository)
        {
            _inGameEventOrderRepository = inGameEventOrderRepository;
        }
        public async Task<ErrorOr<GetInGameEventOrdersResult>> Handle(GetInGameEventOrdersQuery query, CancellationToken cancellationToken)
        {
            var getInGameEventOrdersResult = await _inGameEventOrderRepository.GetInGameEventOrders(query.Page, query.EntriesPerPage);

            if (getInGameEventOrdersResult.IsError)
            {
                return getInGameEventOrdersResult.Errors;
            }

            return new GetInGameEventOrdersResult(getInGameEventOrdersResult.Value);
        }
    }
}
