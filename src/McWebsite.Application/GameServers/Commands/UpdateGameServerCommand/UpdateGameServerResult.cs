﻿using McWebsite.Domain.GameServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Commands.UpdateGameServerCommand
{
    public sealed record UpdateGameServerResult(GameServer GameServer);
}
