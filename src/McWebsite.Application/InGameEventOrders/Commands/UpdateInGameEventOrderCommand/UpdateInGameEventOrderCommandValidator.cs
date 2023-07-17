using FluentValidation;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServerSubscription.Enums;
using McWebsite.Domain.InGameEvent.Enums;

namespace McWebsite.Application.InGameEventOrders.Commands.UpdateInGameEventOrderCommand
{
    public sealed class UpdateInGameEventOrderCommandValidator : AbstractValidator<UpdateInGameEventOrderCommand>
    {
        public UpdateInGameEventOrderCommandValidator()
        {
            RuleFor(x => x.InGameEventOrderId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
            RuleFor(x => x.BoughtInGameEventId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
        }
    }
}
