using ErrorOr;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
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
        Task<GameServer> GetGameServer(Guid gameServerId);
        Task<GameServer> CreateGameServer(GameServer gameServer);
        Task<GameServer> UpdateGameServer(GameServer gameServer);
        Task DeleteGameServer(Guid gameServerId);
    }
}
