using FluentValidation;
using McWebsite.Domain.GameServer.Enums;

namespace McWebsite.Application.GameServers.Commands.UpdateGameServerCommand
{
    public sealed class UpdateGameServerCommandValidator : AbstractValidator<UpdateGameServerCommand>
    {
        public UpdateGameServerCommandValidator()
        {
            RuleFor(x => x.MaximumPlayersNumber)
                .GreaterThan(0);
            RuleFor(x => x.CurrentPlayersNumber)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.ServerLocation)
                .NotEmpty()
                .IsEnumName(typeof(ServerLocation));
            RuleFor(x => x.ServerType)
                .NotEmpty()
                .IsEnumName(typeof(ServerType));
            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
