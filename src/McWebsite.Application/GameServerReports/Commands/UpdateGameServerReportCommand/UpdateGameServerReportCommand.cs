using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand
{
    public sealed record UpdateGameServerReportCommand(Guid GameServerReportId,
                                                       Guid ReportedGameServerId,
                                                       string ReportType,
                                                       string ReportDescription) : IRequest<ErrorOr<UpdateGameServerReportResult>?>;
}
