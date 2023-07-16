using FluentValidation;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServerSubscription.Enums;
using McWebsite.Domain.InGameEvent.Enums;

namespace McWebsite.Application.InGameEvents.Commands.UpdateInGameEventCommand
{
    public sealed class UpdateInGameEventCommandValidator : AbstractValidator<UpdateInGameEventCommand>
    {
        public UpdateInGameEventCommandValidator()
        {
            RuleFor(x => x.InGameEventId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
            RuleFor(x => x.GameServerId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
            RuleFor(x => x.InGameId)
                .GreaterThan(0);
            RuleFor(x => x.InGameEventType)
                .NotEmpty()
                .IsEnumName(typeof(EventType));
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.Price)
                .GreaterThan(0);
        }
    }
}
