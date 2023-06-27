using McWebsite.Domain.GameServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Common.Interfaces.Persistence
{
    public interface IGameServerRepository
    {
        Task<IEnumerable<GameServer>> GetGameServers(int page, int entriesPerPage);
    }
}
