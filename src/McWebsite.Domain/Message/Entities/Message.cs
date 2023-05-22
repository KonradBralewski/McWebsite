using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.MessageModel.ValueObjects;
using McWebsite.Domain.User.ValueObjects;


namespace McWebsite.Domain.MessageModel.Entities
{
    public sealed class Message : Entity<MessageId>
    {
        public ConversationId ConversationId { get; }
        public UserId ReceiverId { get; }
        public UserId ShipperId { get; }
        public string Description { get; }

        private Message(MessageId id,
                        ConversationId conversationId,
                        UserId receiverId,
                        UserId shipperId,
                        string description) : base(id)
        {
            Id = id;
            ConversationId = conversationId;
            ReceiverId = receiverId;
            ShipperId = shipperId;
            Description = description;
        }

        public static Message Create(Guid conversationId, Guid receiverId, Guid shipperId, string description)
        {
            return new Message(MessageId.CreateUnique(),
                               ConversationId.Recreate(receiverId),
                               UserId.Recreate(receiverId),
                               UserId.Recreate(shipperId),
                               description);
        }
    }
}
