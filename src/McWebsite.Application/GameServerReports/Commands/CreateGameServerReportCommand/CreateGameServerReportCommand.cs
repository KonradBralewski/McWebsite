using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Commands.CreateGameServerReportCommand
{
    public sealed record CreateGameServerReportCommand(Guid ReportingUserId,
                                                       Guid ReportedGameServerId,
                                                       string ReportType,
                                                       string ReportDescription) : IRequest<ErrorOr<CreateGameServerReportResult>>;
}
