using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Commands.DeleteGameServerReportCommand
{
    public sealed record DeleteGameServerReportCommand(Guid GameServerReportId) : IRequest<ErrorOr<bool>>;
}
