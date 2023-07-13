using FluentValidation;
using McWebsite.Domain.GameServerReport.Enums;

namespace McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand
{
    public sealed class UpdateGameServerReportCommandValidator : AbstractValidator<UpdateGameServerReportCommand>
    {
        public UpdateGameServerReportCommandValidator()
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
