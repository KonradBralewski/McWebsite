using FluentValidation;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServerSubscription.Enums;

namespace McWebsite.Application.GameServerSubscriptions.Commands.UpdateGameServerSubscriptionCommand
{
    public sealed class UpdateGameServerSubscriptionCommandValidator : AbstractValidator<UpdateGameServerSubscriptionCommand>
    {
        public UpdateGameServerSubscriptionCommandValidator()
        {
            RuleFor(x => x.GameServerId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
            RuleFor(x => x.SubscriptionType)
                .NotEmpty()
                .IsEnumName(typeof(SubscriptionType));
            RuleFor(x => x.InGameSubscriptionId)
                .GreaterThan(0);
            RuleFor(x => x.Price)
                .GreaterThan(0);
            RuleFor(x => x.SubscriptionDescription)
                .NotEmpty();
            RuleFor(x => x.SubscriptionDuration)
                .GreaterThan(TimeSpan.Zero);
        }
    }
}
