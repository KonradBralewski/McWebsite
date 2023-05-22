using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using System.Collections.ObjectModel;
using McWebsite.Domain.MessageModel.Entities;

namespace McWebsite.Domain.Conversation
{
    public sealed class Conversation : AggregateRoot<ConversationId>
    {

        private readonly List<Message> _messages = new();
        public Tuple<UserId, UserId> Perfomers { get; }

        public ReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
        private Conversation(ConversationId id, Tuple<UserId, UserId> performers) : base(id)
        {
            Id = id;
            Perfomers = performers;
        }

        public static Conversation Create(UserId firstPerformer, UserId secondPerformer)
        {
            return new Conversation(ConversationId.CreateUnique(), new Tuple<UserId, UserId>(firstPerformer, secondPerformer));
        }

    }
}
