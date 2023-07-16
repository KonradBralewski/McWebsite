using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEventOrder;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.User;
using McWebsite.Infrastructure.Persistence.Identity;
using McWebsite.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace McWebsite.Infrastructure.Persistence
{
    public sealed class McWebsiteDbContext : IdentityDbContext<McWebsiteIdentityUser>
    {
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        public McWebsiteDbContext(DbContextOptions<McWebsiteDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        }

        public DbSet<User> DomainUsers { get; set; }
        public DbSet<GameServer> GameServers { get; set; }
        public DbSet<GameServerReport> GameServersReports { get; set; }
        public DbSet<GameServerSubscription> GameServerSubscriptions { get; set; }
        public DbSet<InGameEvent> InGameEvents { get; set; }
        public DbSet<InGameEventOrder> InGameEventOrders { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }


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
