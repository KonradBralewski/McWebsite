using FluentValidation;
using FluentValidation.Results;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServerReport.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand
{
    public sealed class CreateGameServerReportCommandValidator : AbstractValidator<CreateGameServerReportCommand>
    {
        public CreateGameServerReportCommandValidator()
        {
            RuleFor(x => x.ReportedGameServerId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id.ToString(), out _));
            RuleFor(x => x.ReportType)
                .NotEmpty()
                .IsEnumName(typeof(ReportType));
            RuleFor(x => x.ReportDescription)
                .NotEmpty();
        }

    }
}
