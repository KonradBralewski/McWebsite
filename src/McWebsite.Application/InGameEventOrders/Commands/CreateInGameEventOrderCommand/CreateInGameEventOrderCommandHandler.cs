using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.InGameEvent.ValueObjects;
using McWebsite.Domain.InGameEventOrder;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand
{
    public sealed class CreateInGameEventOrderCommandHandler : IRequestHandler<CreateInGameEventOrderCommand, ErrorOr<CreateInGameEventOrderResult>>
    {
        private readonly IInGameEventOrderRepository _inGameEventOrderRepository;
        private readonly IInGameEventRepository _inGameEventRepository;
        public CreateInGameEventOrderCommandHandler(IInGameEventOrderRepository inGameEventOrderRepository, IInGameEventRepository inGameEventRepository)
        {
            _inGameEventOrderRepository = inGameEventOrderRepository;
            _inGameEventRepository = inGameEventRepository;
        }
        public async Task<ErrorOr<CreateInGameEventOrderResult>> Handle(CreateInGameEventOrderCommand command, CancellationToken cancellationToken)
        {
            var inGameEventSearchResult = await _inGameEventRepository.GetInGameEvent(InGameEventId.Create(command.BoughtInGameEventId));

            if (inGameEventSearchResult.IsError)
            {
                return inGameEventSearchResult.Errors;
            }

            InGameEventOrder toBeAdded = InGameEventOrder.Create(command.BuyingUserId,
                                                                 command.BoughtInGameEventId,
                                                                 DateTime.UtcNow,
                                                                 DateTime.UtcNow);

            var creationResult = await _inGameEventOrderRepository.CreateInGameEventOrder(toBeAdded);

            if (creationResult.IsError)
            {
                return creationResult.Errors;
            }

            InGameEventOrder createdInGameEventOrder = creationResult.Value;

            return new CreateInGameEventOrderResult(createdInGameEventOrder);
        }
    }
}
