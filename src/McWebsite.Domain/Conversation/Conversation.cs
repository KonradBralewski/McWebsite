﻿using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.Conversation.ValueObjects;
using System.Collections.ObjectModel;
using McWebsite.Domain.MessageModel.ValueObjects;
using McWebsite.Domain.Conversation.Events;

namespace McWebsite.Domain.Conversation
{
    public static class ConversationEventsExtensions
    {
        public static void Start(this Conversation conversation, string firstMessageContent)
        {
            conversation.AddDomainEvent(new ConversationStartedEvent((ConversationId)conversation.Id,
                                                                     conversation.Participants,
                                                                     firstMessageContent));
        }
        public static void Delete(this Conversation conversation)
        {
            conversation.AddDomainEvent(new ConversationDeletedEvent());
        }
    }
    public sealed class Conversation : AggregateRoot<ConversationId, Guid>
    {

        private readonly IList<MessageId> _messageIds;
        public ConversationParticipants Participants { get; private set; }

        public IReadOnlyCollection<MessageId> MessageIds => (_messageIds as List<MessageId>)!.AsReadOnly();

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }
        private Conversation(ConversationId id,
                             ConversationParticipants participants,
                             List<MessageId> messagesIds,
                             DateTime createdDateTime,
                             DateTime updatedDateTime) : base(id)
        {
            Id = id;
            Participants = participants;
            _messageIds = messagesIds;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Conversation Create(Guid firstParticipant,
                                          Guid secondParticipant,
                                          DateTime createdDateTime,
                                          DateTime updatedDateTime)
        {
            return new Conversation(ConversationId.CreateUnique(),
                                    ConversationParticipants.Create(firstParticipant, secondParticipant),
                                    new List<MessageId>(),
                                    createdDateTime,
                                    updatedDateTime);
        }

        public static Conversation Recreate(Guid id,
                                            Guid firstParticipant,
                                            Guid secondParticipant,
                                            IEnumerable<Guid> messagesIds,
                                            DateTime createdDateTime,
                                            DateTime updatedDateTime)
        {
            return new Conversation(ConversationId.Create(id),
                                    ConversationParticipants.Create(firstParticipant, secondParticipant),
                                    messagesIds.Select(mi => MessageId.Create(mi)).ToList(),
                                    createdDateTime,
                                    updatedDateTime);
        }

        /// <summary>
        /// Constructor that will be used by EF Core, EF Core is not able to setup navigation property for Tuple<UserId, UserId>
        /// </summary>
#pragma warning disable CS8618
        private Conversation()
        {

        }

#pragma warning restore CS8618

    }
}
