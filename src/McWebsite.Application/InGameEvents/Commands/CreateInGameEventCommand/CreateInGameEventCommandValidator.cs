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

namespace McWebsite.Application.InGameEvents.Commands.CreateInGameEventCommand
{
    public sealed class CreateInGameEventCommandValidator : AbstractValidator<CreateInGameEventCommand>
    {
        public CreateInGameEventCommandValidator()
        {
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
