using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;
using McWebsite.Application.Common.Utilities;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerSubscription.Enums;
using McWebsite.Domain.InGameEvent.ValueObjects;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEvent.Enums;
using McWebsite.Domain.InGameEventOrder.ValueObjects;
using McWebsite.Domain.InGameEventOrder;

namespace McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand
{
    public sealed class UpdateInGameEventOrderCommandHandler : IRequestHandler<UpdateInGameEventOrderCommand, ErrorOr<UpdateInGameEventOrderResult>?>
    {
        private readonly IInGameEventOrderRepository _inGameEventOrderRepository;
        private readonly IInGameEventRepository _inGameEventRepository;
        public UpdateInGameEventOrderCommandHandler(IInGameEventOrderRepository inGameEventOrderRepository,
                                                    IInGameEventRepository inGameEventRepository)
        {
            _inGameEventOrderRepository = inGameEventOrderRepository;
            _inGameEventRepository = inGameEventRepository;
        }
        public async Task<ErrorOr<UpdateInGameEventOrderResult>?> Handle(UpdateInGameEventOrderCommand command, CancellationToken cancellationToken)
        {
            var inGameEventOrderSearchResult = await _inGameEventOrderRepository.GetInGameEventOrder(
                InGameEventOrderId.Create(command.InGameEventId));

            if (inGameEventOrderSearchResult.IsError)
            {
                return inGameEventOrderSearchResult.Errors;
            }

            var inGameEventSearchResult = await _inGameEventRepository.GetInGameEvent(InGameEventId.Create(command.BoughtInGameEventId));

            if (inGameEventSearchResult.IsError)
            {
                return inGameEventSearchResult.Errors;
            }

            InGameEventOrder foundInGameEventOrder = inGameEventOrderSearchResult.Value;

            if (ApplyModfications(foundInGameEventOrder, command) is not InGameEventOrder inGameEventOrderAfterUpdate)
            {
                return null;
            }

            var updatedInGameEventOrderResult = await _inGameEventOrderRepository.UpdateInGameEventOrder(inGameEventOrderAfterUpdate);

            if (updatedInGameEventOrderResult.IsError)
            {
                return updatedInGameEventOrderResult.Errors;
            }

            InGameEventOrder updatedInGameEventOrder = updatedInGameEventOrderResult.Value;


            return new UpdateInGameEventOrderResult(updatedInGameEventOrder);
        }

        private InGameEventOrder? ApplyModfications(InGameEventOrder toBeUpdated, UpdateInGameEventOrderCommand command)
        {
            bool hasSomethingChanged = false;

            if (toBeUpdated.BoughtInGameEventId.Value != command.BoughtInGameEventId)
            {
                hasSomethingChanged = true;
            }

            if (!hasSomethingChanged)
            {
                return null;
            }

            return InGameEventOrder.Recreate(toBeUpdated.Id.Value,
                                       toBeUpdated.BuyingUserId.Value,
                                       command.BoughtInGameEventId,
                                       toBeUpdated.OrderDate,
                                       DateTime.UtcNow);
        }
    }
}
