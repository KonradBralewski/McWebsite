using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEvent.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace McWebsite.Infrastructure.Persistence.Configurations
{
    public sealed class InGameEventConfigurations : IEntityTypeConfiguration<InGameEvent>
    {
        public void Configure(EntityTypeBuilder<InGameEvent> builder)
        {
            builder.ToTable("InGameEvents");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever()
                .HasConversion(
                inGameEventId => inGameEventId.Value,
                value => InGameEventId.Create(value));

            builder.Property(x => x.GameServerId)
                .IsRequired()
                .ValueGeneratedNever()
                .HasConversion(
                gamerServerId => gamerServerId.Value,
                value => GameServerId.Create(value));

            builder.Property(x => x.InGameId)
                .IsRequired();

            builder.Property(x => x.InGameEventType)
                .IsRequired()
                .ValueGeneratedNever()
                .HasConversion(
                inGameEventType => inGameEventType.Value,
                value => InGameEventType.Create(value));

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.UpdatedDateTime)
                .IsRequired();
        }
    }
}
