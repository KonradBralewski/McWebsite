using ErrorOr;
using McWebsite.Domain.GameServer.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Commands.CreateGameServerCommand
{
    public sealed record CreateGameServerCommand(
        int MaximumPlayersNumber,
        string ServerLocation,
        string ServerType,
        string Description) : IRequest<ErrorOr<CreateGameServerResult>>;
}
