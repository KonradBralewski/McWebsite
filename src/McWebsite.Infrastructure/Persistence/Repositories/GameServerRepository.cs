using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.User;
using Microsoft.EntityFrameworkCore;
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
        public async Task<GameServer> GetGameServer(GameServerId gameServerId)
        {
            return await _dbContext.GameServers.FirstOrDefaultAsync(gs => gs.Id == gameServerId);
        }

        public Task<GameServer> CreateGameServer(GameServer gameServer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGameServer(GameServer gameServer)
        {
            throw new NotImplementedException();
        }


        public Task<GameServer> UpdateGameServer(GameServer gameServer)
        {
            throw new NotImplementedException();
        }
    }
}
