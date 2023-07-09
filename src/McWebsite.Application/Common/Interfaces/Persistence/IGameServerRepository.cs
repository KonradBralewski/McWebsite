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
        Task<ErrorOr<IEnumerable<GameServer>>> GetGameServers(int page, int entriesPerPage);
        Task<ErrorOr<GameServer>> GetGameServer(GameServerId gameServerId);
        Task<ErrorOr<GameServer>> CreateGameServer(GameServer gameServer);
        Task<ErrorOr<GameServer>> UpdateGameServer(GameServer gameServer);
        Task DeleteGameServer(GameServer gameServer);
    }
}
