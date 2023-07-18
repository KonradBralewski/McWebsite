using FluentValidation;
using FluentValidation.Results;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServerReport.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Conversations.Commands.CreateConversationCommand
{
    public sealed class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
    {
        public CreateConversationCommandValidator()
        {
            RuleFor(x => x.FirstParticipantId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
            RuleFor(x => x.SecondParticipantId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
        }

    }
}
