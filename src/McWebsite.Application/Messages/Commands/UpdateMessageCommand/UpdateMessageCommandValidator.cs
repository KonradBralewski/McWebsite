using FluentValidation;
using McWebsite.Domain.GameServer.Enums;

namespace McWebsite.Application.Messages.Commands.UpdateMessageCommand
{
    public sealed class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(x => x.MessageId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));

            RuleFor(x => x.MessageContent)
                .NotEmpty();
        }
    }
}
