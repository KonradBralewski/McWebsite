using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.InGameEventOrder.ValueObjects;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrderQuery
{
    public sealed class GetInGameEventOrderQueryHandler : IRequestHandler<GetInGameEventOrderQuery, ErrorOr<GetInGameEventOrderResult>>
    {
        private readonly IInGameEventOrderRepository _inGameEventOrderRepository;
        public GetInGameEventOrderQueryHandler(IInGameEventOrderRepository inGameEventOrderRepository)
        {
            _inGameEventOrderRepository = inGameEventOrderRepository;
        }
        public async Task<ErrorOr<GetInGameEventOrderResult>> Handle(GetInGameEventOrderQuery query, CancellationToken cancellationToken)
        {
            var getInGameEventOrderResult = await _inGameEventOrderRepository.GetInGameEventOrder(
                InGameEventOrderId.Create(query.InGameEventOrderId));

            if (getInGameEventOrderResult.IsError)
            {
                return getInGameEventOrderResult.Errors;
            }

            return new GetInGameEventOrderResult(getInGameEventOrderResult.Value);
        }
    }
}
