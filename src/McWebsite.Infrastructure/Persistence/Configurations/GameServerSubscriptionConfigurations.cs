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

            builder.Property(x => x.BuyingPlayerId)
                .ValueGeneratedNever()
                .IsRequired()
                .HasConversion(
                reportingUserId => reportingUserId.Value,
                value => UserId.Create(value));

            builder.Property(x => x.SubscriptionDescription)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.SubscriptionStartDate)
                .IsRequired();

            builder.Property(x => x.SubscriptionEndDate)
                .IsRequired();

            builder.Property(x => x.UpdatedDateTime)
               .IsRequired();

        }
    }
}
