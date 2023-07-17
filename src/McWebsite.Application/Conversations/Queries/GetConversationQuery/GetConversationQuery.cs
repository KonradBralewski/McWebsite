using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Conversations.Queries.GetConversationQuery
{
    public sealed record GetConversationQuery(Guid ConversationId) : IRequest<ErrorOr<GetConversationResult>>;
}
