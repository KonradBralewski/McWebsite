using FluentValidation;
using FluentValidation.Results;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServerSubscription.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerSubscriptions.Commands.CreateGameServerSubscriptionCommand
{
    public sealed class CreateGameServerSubscriptionCommandValidator : AbstractValidator<CreateGameServerSubscriptionCommand>
    {
        public CreateGameServerSubscriptionCommandValidator()
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
