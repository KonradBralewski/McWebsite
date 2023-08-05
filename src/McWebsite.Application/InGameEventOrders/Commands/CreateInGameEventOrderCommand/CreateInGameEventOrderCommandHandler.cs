using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.InGameEvent.ValueObjects;
using McWebsite.Domain.InGameEventOrder;
using McWebsite.Domain.User.ValueObjects;
using MediatR;

namespace McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand
{
    public sealed class CreateInGameEventOrderCommandHandler : IRequestHandler<CreateInGameEventOrderCommand, ErrorOr<CreateInGameEventOrderResult>>
    {
        private readonly IInGameEventOrderRepository _inGameEventOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IInGameEventRepository _inGameEventRepository;
        public CreateInGameEventOrderCommandHandler(IInGameEventOrderRepository inGameEventOrderRepository, IInGameEventRepository inGameEventRepository, IUserRepository userRepository)
        {
            _inGameEventOrderRepository = inGameEventOrderRepository;
            _inGameEventRepository = inGameEventRepository;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<CreateInGameEventOrderResult>> Handle(CreateInGameEventOrderCommand command, CancellationToken cancellationToken)
        {
            var buyingUserSearchResult = await _userRepository.GetUser(UserId.Create(command.BuyingUserId));

            if (buyingUserSearchResult.IsError)
            {
                return buyingUserSearchResult.Errors;
            }

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
