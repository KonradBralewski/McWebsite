using McWebsite.Domain.GameServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Queries.GetGameServers
{
    public sealed record GetGameServersResult(IEnumerable<GameServer> GameServers);
}
