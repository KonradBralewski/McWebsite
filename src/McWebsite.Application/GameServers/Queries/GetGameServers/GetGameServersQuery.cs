using ErrorOr;
using McWebsite.Domain.GameServer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Queries.GetGameServers
{
    public sealed record GetGameServersQuery(int page, int entriesPerPage) : IRequest<ErrorOr<IEnumerable<GameServer>>>;
}
