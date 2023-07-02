using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Persistence.Repositories
{
    internal sealed class GameServerRepository : IGameServerRepository
    {
        private readonly McWebsiteDbContext _dbContext;

        public GameServerRepository(McWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GameServer>> GetGameServers(int page, int entriesPerPage)
        {
            await Task.CompletedTask;
            return _dbContext.GameServers.Skip(page * entriesPerPage).Take(entriesPerPage);
        }
    }
}
