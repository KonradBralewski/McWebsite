using McWebsite.Domain.GameServerSubscription;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.User.ValueObjects;

namespace McWebsite.Infrastructure.Persistence.Configurations
{
    public sealed class GameServerSubscriptionConfigurations : IEntityTypeConfiguration<GameServerSubscription>
    {
        public void Configure(EntityTypeBuilder<GameServerSubscription> builder)
        {
            builder.ToTable("GameServerSubscriptions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => GameServerSubscriptionId.Create(value));

            builder.Property(x => x.GameServerId)
                .ValueGeneratedNever()
                .IsRequired()
                .HasConversion(
                gameServerId => gameServerId.Value,
                value => GameServerId.Create(value));

            builder.Property(x => x.SubscriptionType)
                .ValueGeneratedNever()
                .IsRequired()
                .HasConversion(
                subscriptionType => subscriptionType.Value,
                value => GameServerSubscriptionType.Create(value));

            builder.Property(x => x.InGameSubscriptionId)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired()
                .HasDefaultValue(50.0f);

            builder.Property(x => x.SubscriptionDescription)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.SubscriptionDuration)
                .IsRequired()
                .HasDefaultValue(TimeSpan.FromDays(1));

            builder.Property(x => x.CreatedDateTime)
               .IsRequired();

            builder.Property(x => x.UpdatedDateTime)
               .IsRequired();

        }
    }
}
