﻿using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.MessageModel.ValueObjects;
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
    public sealed class MessageConfigurations : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
               id => id.Value,
               value => MessageId.Create(value));

            builder.Property(x => x.ConversationId)
                .IsRequired()
                .ValueGeneratedNever()
                .HasConversion(
                conversationId => conversationId.Value,
                value => ConversationId.Create(value));

            builder.Property(x => x.ReceiverId)
               .IsRequired()
               .ValueGeneratedNever()
               .HasConversion(
               receiverId => receiverId.Value,
               value => UserId.Create(value));

            builder.Property(x => x.ShipperId)
               .IsRequired()
               .ValueGeneratedNever()
               .HasConversion(
               shipperId => shipperId.Value,
               value => UserId.Create(value));

            builder.Property(x => x.MessageContent)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.SentDateTime)
                .IsRequired();

            builder.Property(x => x.UpdatedDateTime)
                .IsRequired();

        }
    }
}
