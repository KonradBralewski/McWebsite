using McWebsite.Domain.GameServerReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerReports.Queries.GetGameServersReportsQuery
{
    public sealed record GetGameServersReportsResult(IEnumerable<GameServerReport> GameServersReports);
}
