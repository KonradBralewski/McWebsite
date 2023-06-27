using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Persistence
{
    internal sealed class GameServerRepository : IGameServerRepository
    {
        private static readonly List<GameServer> _gameServers = new List<GameServer>();

        public async Task<IEnumerable<GameServer>> GetGameServers(int page, int entriesPerPage)
        {
            return _gameServers.Skip(page * entriesPerPage).Take(entriesPerPage);
        }
    }
}
