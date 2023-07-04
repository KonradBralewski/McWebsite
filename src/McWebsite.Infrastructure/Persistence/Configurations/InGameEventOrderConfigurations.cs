using McWebsite.Domain.InGameEventModel.ValueObjects;
using McWebsite.Domain.InGameEventOrder;
using McWebsite.Domain.InGameEventOrder.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace McWebsite.Infrastructure.Persistence.Configurations
{
    public sealed class InGameEventOrderConfigurations : IEntityTypeConfiguration<InGameEventOrder>
    {
        public void Configure(EntityTypeBuilder<InGameEventOrder> builder)
        {
            builder.ToTable("InGameEventOrders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
               id => id.Value,
               value => InGameEventOrderId.Create(value));

            builder.Property(x => x.BuyingUserId)
               .IsRequired()
               .ValueGeneratedNever()
               .HasConversion(
               buyingUserId => buyingUserId.Value,
               value => UserId.Create(value));

            builder.Property(x => x.BoughtInGameEventId)
               .IsRequired()
               .ValueGeneratedNever()
               .HasConversion(
               boughtInGameEventId => boughtInGameEventId.Value,
               value => InGameEventId.Create(value));
        }
    }
}
