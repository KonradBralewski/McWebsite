using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Commands.UpdateGameServerCommand
{
    public sealed record UpdateGameServerCommand(Guid GameServerId,
                                                 int MaximumPlayersNumber,
                                                 int CurrentPlayersNumber,
                                                 string ServerLocation,
                                                 string ServerType,
                                                 string Description) : IRequest<ErrorOr<UpdateGameServerResult>?>;
}
