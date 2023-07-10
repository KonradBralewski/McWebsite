using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Queries.GetGameServerReportQuery
{
    public sealed record GetGameServerReportQuery(Guid GameServerReportId) : IRequest<ErrorOr<GetGameServerReportResult>>;
}
