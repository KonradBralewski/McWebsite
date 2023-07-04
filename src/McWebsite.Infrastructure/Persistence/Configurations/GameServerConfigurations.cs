using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Persistence.Configurations
{
    public sealed class GameServerConfigurations : IEntityTypeConfiguration<GameServer>
    {
        public void Configure(EntityTypeBuilder<GameServer> builder)
        {
            builder.ToTable("GameServers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => GameServerId.Create(value));
 
            builder.Property(x => x.MaximumPlayersNumber)
                .IsRequired();

            builder.Property(x => x.CurrentPlayersNumber)
                .IsRequired();

            builder.Property(x => x.ServerLocation)
               .IsRequired()
               .HasConversion(
                location => location.Value,
               value => GameServerLocation.Create(value));

            builder.Property(x => x.ServerType)
               .IsRequired()
               .HasConversion(
                serverType => serverType.Value,
                value => GameServerType.Create(value));

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.UpdatedDateTime)
                .IsRequired();
        }
    }
}
