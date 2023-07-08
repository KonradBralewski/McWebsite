using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Commands.DeleteGameServerCommand
{
    public sealed class DeleteGameServerCommandValidator : AbstractValidator<DeleteGameServerCommand>
    {
        public DeleteGameServerCommandValidator()
        {
            RuleFor(x => x.GameServerId).NotEmpty().WithMessage("GameServerId cannot be empty.");

            RuleFor(x => x.GameServerId).Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("GameServerId has to be a valid guid value.");
        }
    }
}
