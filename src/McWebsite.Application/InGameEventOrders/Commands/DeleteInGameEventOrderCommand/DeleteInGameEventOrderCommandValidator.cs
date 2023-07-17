using FluentValidation;

namespace McWebsite.Application.InGameEventOrders.Commands.DeleteInGameEventOrderCommand
{
    public sealed class DeleteInGameEventOrderCommandValidator : AbstractValidator<DeleteInGameEventOrderCommand>
    {
        public DeleteInGameEventOrderCommandValidator()
        {
            RuleFor(x => x.InGameEventOrderId).NotEmpty()
                .WithMessage("InGameEventOrderId cannot be empty.");
            RuleFor(x => x.InGameEventOrderId).Must(id => Guid.TryParse(id.ToString(), out _))
                .WithMessage("InGameEventOrderId has to be a valid guid value.");
        }
    }
}
