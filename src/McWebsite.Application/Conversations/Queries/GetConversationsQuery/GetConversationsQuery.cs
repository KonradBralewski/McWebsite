using ErrorOr;
using MediatR;

namespace McWebsite.Application.Conversations.Queries.GetConversationsQuery
{
    public sealed record GetConversationsQuery(int Page, int EntriesPerPage) : IRequest<ErrorOr<GetConversationsResult>>;
}
