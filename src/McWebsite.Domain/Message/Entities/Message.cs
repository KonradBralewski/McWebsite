using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.MessageModel.ValueObjects;
using McWebsite.Domain.User.ValueObjects;


namespace McWebsite.Domain.MessageModel.Entities
{
    public sealed class Message : Entity<MessageId>
    {
        public ConversationId ConversationId { get; private set; }
        public UserId ReceiverId { get; private set; }
        public UserId ShipperId { get; private set; }
        public string MessageContent { get; private set; }
        public DateTime SentDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private Message(MessageId id,
                        ConversationId conversationId,
                        UserId receiverId,
                        UserId shipperId,
                        string messageContent,
                        DateTime sentDateTime,
                        DateTime updatedDateTime) : base(id)
        {
            Id = id;
            ConversationId = conversationId;
            ReceiverId = receiverId;
            ShipperId = shipperId;
            MessageContent = messageContent;
            SentDateTime = sentDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Message Create(Guid conversationId,
                                     Guid receiverId,
                                     Guid shipperId,
                                     string messageContent,
                                     DateTime sentDateTime,
                                     DateTime updatedDateTime)
        {
            return new Message(MessageId.CreateUnique(),
                               ConversationId.Create(receiverId),
                               UserId.Create(receiverId),
                               UserId.Create(shipperId),
                               messageContent,
                               sentDateTime,
                               updatedDateTime);
        }
    }
}
