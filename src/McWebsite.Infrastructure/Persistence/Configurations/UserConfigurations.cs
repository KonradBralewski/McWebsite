using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.User;
using McWebsite.Domain.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

            builder.Ignore(x => x.Email); // Will be keeped in track / received using ASP.NET Identity

            builder.Ignore(x => x.Password); // Will be keeped in track / received using ASP.NET Identity

            builder.Property(x => x.MinecraftAccountId)
                .ValueGeneratedNever()
                .IsRequired(false)
                .HasConversion(
                minecraftAccountId => minecraftAccountId.Value,
                value => MinecraftAccountId.Create(value));

            builder.Property(x => x.CreatedDateTime)
                .IsRequired();

            builder.Property(x => x.UpdatedDateTime)
                .IsRequired();
        }
    }
}
