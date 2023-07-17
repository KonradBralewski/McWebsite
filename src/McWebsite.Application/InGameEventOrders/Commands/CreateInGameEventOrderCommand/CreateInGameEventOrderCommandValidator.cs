using FluentValidation;
using FluentValidation.Results;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServerSubscription.Enums;
using McWebsite.Domain.InGameEvent.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.InGameEventOrders.Commands.CreateInGameEventOrderCommand
{
    public sealed class CreateInGameEventOrderCommandValidator : AbstractValidator<CreateInGameEventOrderCommand>
    {
        public CreateInGameEventOrderCommandValidator()
        {
            RuleFor(x => x.BuyingUserId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
            RuleFor(x => x.BoughtInGameEventId)
                  .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
        }

    }
}
