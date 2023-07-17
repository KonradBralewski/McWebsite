using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEvent.ValueObjects;
using McWebsite.Domain.InGameEventOrder;
using McWebsite.Domain.InGameEventOrder.ValueObjects;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Commands.DeleteInGameEventOrderCommand
{
    public sealed class DeleteInGameEventOrderCommandHandler : IRequestHandler<DeleteInGameEventOrderCommand, ErrorOr<bool>>
    {
        private readonly IInGameEventOrderRepository _inGameEventOrderRepository;
        public DeleteInGameEventOrderCommandHandler(IInGameEventOrderRepository inGameEventOrderRepository)
        {
            _inGameEventOrderRepository = inGameEventOrderRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteInGameEventOrderCommand request, CancellationToken cancellationToken)
        {
            var inGameEventOrderSearchResult = await _inGameEventOrderRepository.GetInGameEventOrder(
                InGameEventOrderId.Create(request.InGameEventOrderId));

            if (inGameEventOrderSearchResult.IsError)
            {
                return inGameEventOrderSearchResult.Errors;
            }

            InGameEventOrder inGameEventOrder = inGameEventOrderSearchResult.Value;

            await _inGameEventOrderRepository.DeleteInGameEventOrder(inGameEventOrder);

            return true;
        }
    }
}
