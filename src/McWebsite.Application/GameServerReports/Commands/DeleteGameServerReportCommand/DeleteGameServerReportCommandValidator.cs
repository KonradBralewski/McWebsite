using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Commands.DeleteGameServerReportCommand
{
    public sealed class DeleteGameServerReportCommandValidator : AbstractValidator<DeleteGameServerReportCommand>
    {
        public DeleteGameServerReportCommandValidator()
        {
            RuleFor(x => x.GameServerReportId).NotEmpty().WithMessage("GameServerReportId cannot be empty.");
            RuleFor(x => x.GameServerReportId).Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("GameServerReportId has to be a valid guid value.");
        }
    }
}
