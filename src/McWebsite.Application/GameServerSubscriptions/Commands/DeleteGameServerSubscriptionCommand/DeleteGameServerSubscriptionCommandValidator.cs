using FluentValidation;

namespace McWebsite.Application.GameServerSubscriptions.Commands.DeleteGameServerSubscriptionCommand
{
    public sealed class DeleteGameServerSubscriptionCommandValidator : AbstractValidator<DeleteGameServerSubscriptionCommand>
    {
        public DeleteGameServerSubscriptionCommandValidator()
        {
            RuleFor(x => x.GameServerSubscriptionId).NotEmpty()
                .WithMessage("GameServerSubscriptionId cannot be empty.");
            RuleFor(x => x.GameServerSubscriptionId).Must(id => Guid.TryParse(id.ToString(), out _))
                .WithMessage("GameServerSubscriptionId has to be a valid guid value.");
        }
    }
}
