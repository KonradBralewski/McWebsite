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
        public UserId FirstParticipant { get; private set; }
        public UserId SecondParticipant { get; private set; }

        private ConversationParticipants(UserId firstParticipant, UserId secondParticipant)
        {
            FirstParticipant = firstParticipant;
            SecondParticipant = secondParticipant;
        }

        public static ConversationParticipants Create(Guid firstParticipantId, Guid secondParticipantId)
        {
            return new ConversationParticipants(UserId.Create(firstParticipantId), UserId.Create(secondParticipantId));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstParticipant;
            yield return SecondParticipant;
        }
    }
}
