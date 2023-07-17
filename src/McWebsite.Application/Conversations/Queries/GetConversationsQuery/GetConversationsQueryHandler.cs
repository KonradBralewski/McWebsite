using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Conversations.Queries.GetConversationsQuery
{
    public sealed class GetConversationsQueryHandler : IRequestHandler<GetConversationsQuery, ErrorOr<GetConversationsResult>>
    {
        private readonly IConversationRepository _conversationRepository;
        public GetConversationsQueryHandler(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public async Task<ErrorOr<GetConversationsResult>> Handle(GetConversationsQuery query, CancellationToken cancellationToken)
        {
            var getConversationsResult = await _conversationRepository.GetConversations(query.Page, query.EntriesPerPage);

            if (getConversationsResult.IsError)
            {
                return getConversationsResult.Errors;
            }

            return new GetConversationsResult(getConversationsResult.Value);
        }
    }
}
