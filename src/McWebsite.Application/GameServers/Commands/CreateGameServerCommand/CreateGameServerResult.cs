using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Commands.CreateGameServerCommand
{
    public sealed record CreateGameServerResult(Guid Id,
                                                int MaximumPlayersNumber,
                                                int CurrentPlayersNumber,
                                                string ServerLocation,
                                                string ServerType,
                                                string Description);
}
