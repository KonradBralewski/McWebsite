using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Conversations.Commands.DeleteConversationCommand
{
    public sealed class DeleteConversationCommandValidator : AbstractValidator<DeleteConversationCommand>
    {
        public DeleteConversationCommandValidator()
        {
            RuleFor(x => x.ConversationId).NotEmpty().WithMessage("ConversationId cannot be empty.");
            RuleFor(x => x.ConversationId).Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("ConversationId has to be a valid guid value.");
        }
    }
}
