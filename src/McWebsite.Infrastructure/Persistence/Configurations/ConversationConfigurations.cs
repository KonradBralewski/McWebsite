using McWebsite.Domain.GameServer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.MessageModel.ValueObjects;
using McWebsite.Domain.User.ValueObjects;

namespace McWebsite.Infrastructure.Persistence.Configurations
{
    public sealed class ConversationConfigurations : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ToTable("Conversations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => ConversationId.Create(value));

            builder.OwnsOne(x => x.Participants, pb =>
            {
                pb.Property(p => p.FirstParticipantId)
                    .IsRequired()
                    .HasColumnName("FirstParticipantId")
                    .HasConversion(
                    participant => participant.Value,
                    value => UserId.Create(value));

                pb.Property(p => p.SecondParticipantId)
                   .IsRequired()
                   .HasColumnName("SecondParticipantId")
                   .HasConversion(
                   participant => participant.Value,
                   value => UserId.Create(value));
            });

            builder.OwnsMany(x => x.MessageIds, mb =>
            {
                mb.ToTable("ConversationMessageIds");

                mb.WithOwner().HasForeignKey("ConversationId");

                mb.HasKey("Id");

                mb.Property(x => x.Value)
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasColumnName("MessageId");
            });

            builder.Metadata.FindNavigation(nameof(Conversation.MessageIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(x => x.CreatedDateTime)
                .IsRequired();

            builder.Property(x => x.UpdatedDateTime)
                .IsRequired();
        }
    }
}
