using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServerReport;
using McWebsite.Infrastructure.Persistence.Interceptors;
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
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        public McWebsiteDbContext(DbContextOptions<McWebsiteDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        }

        public DbSet<GameServer> GameServers { get; set; }
        public DbSet<GameServerReport> GameServersReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(McWebsiteDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
