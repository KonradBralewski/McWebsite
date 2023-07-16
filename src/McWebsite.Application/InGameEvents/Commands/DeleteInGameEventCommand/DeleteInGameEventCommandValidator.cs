using FluentValidation;

namespace McWebsite.Application.InGameEvents.Commands.DeleteInGameEventCommand
{
    public sealed class DeleteInGameEventCommandValidator : AbstractValidator<DeleteInGameEventCommand>
    {
        public DeleteInGameEventCommandValidator()
        {
            RuleFor(x => x.InGameEventId).NotEmpty()
                .WithMessage("InGameEventId cannot be empty.");
            RuleFor(x => x.InGameEventId).Must(id => Guid.TryParse(id.ToString(), out _))
                .WithMessage("InGameEventId has to be a valid guid value.");
        }
    }
}
