using FluentValidation;
using McWebsite.Domain.GameServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Commands.CreateGameServerCommand
{
    public sealed class CreateGameServerCommandValidator : AbstractValidator<CreateGameServerCommand>
    {
        public CreateGameServerCommandValidator()
        {
            RuleFor(x => x.MaximumPlayersNumber)
                .GreaterThan(0);
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
