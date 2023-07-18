using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Conversation.ValueObjects
{
    public sealed class ConversationParticipants : ValueObject
    {
        public UserId FirstParticipantId { get; private set; }
        public UserId SecondParticipantId { get; private set; }

        private ConversationParticipants(UserId firstParticipant, UserId secondParticipant)
        {
            FirstParticipantId = firstParticipant;
            SecondParticipantId = secondParticipant;
        }

#pragma warning disable CS8618
        private ConversationParticipants()
#pragma warning restore CS8618
        { }

        public static ConversationParticipants Create(Guid firstParticipantId, Guid secondParticipantId)
        {
            return new ConversationParticipants(UserId.Create(firstParticipantId), UserId.Create(secondParticipantId));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstParticipantId;
            yield return SecondParticipantId;
        }
    }
}
