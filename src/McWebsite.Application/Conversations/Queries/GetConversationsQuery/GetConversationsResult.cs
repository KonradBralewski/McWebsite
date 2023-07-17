using McWebsite.Domain.Conversation;
using McWebsite.Domain.GameServerReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Conversations.Queries.GetConversationsQuery
{
    public sealed record GetConversationsResult(IEnumerable<Conversation> Conversations);
}
