using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Messages.Commands.DeleteMessageCommand
{
    public sealed class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty().WithMessage("MessageId cannot be empty.");

            RuleFor(x => x.MessageId).Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("MessageId has to be a valid guid value.");
        }
    }
}
