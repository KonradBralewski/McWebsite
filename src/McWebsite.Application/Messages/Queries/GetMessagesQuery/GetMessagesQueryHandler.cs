using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;

namespace McWebsite.Application.Messages.Queries.GetMessagesQuery
{
    public sealed class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, ErrorOr<GetMessagesResult>>
    {
        private readonly IMessageRepository _messageRepository;
        public GetMessagesQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<ErrorOr<GetMessagesResult>> Handle(GetMessagesQuery query, CancellationToken cancellationToken)
        {
            var getMessagesResult = await _messageRepository.GetMessages(query.Page, query.EntriesPerPage);

            if (getMessagesResult.IsError)
            {
                return getMessagesResult.Errors;
            }

            return new GetMessagesResult(getMessagesResult.Value);
        }
    }
}
