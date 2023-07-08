using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Commands.DeleteGameServerCommand
{
    public sealed record DeleteGameServerCommand(Guid GameServerId) : IRequest<ErrorOr<bool>>;
}
