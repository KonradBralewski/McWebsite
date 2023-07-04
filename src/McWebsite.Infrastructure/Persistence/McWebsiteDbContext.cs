using McWebsite.Domain.GameServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Persistence
{
    public sealed class McWebsiteDbContext : DbContext
    {
        public McWebsiteDbContext(DbContextOptions<McWebsiteDbContext> options) : base(options) { }

        public DbSet<GameServer> GameServers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(McWebsiteDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
