using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.MessageModel.ValueObjects;
using MediatR;

namespace McWebsite.Application.Messages.Queries.GetMessageQuery
{
    public sealed class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, ErrorOr<GetMessageResult>>
    {
        private readonly IMessageRepository _messageRepository;
        public GetMessageQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<ErrorOr<GetMessageResult>> Handle(GetMessageQuery query, CancellationToken cancellationToken)
        {
            var getMessageResult = await _messageRepository.GetMessage(MessageId.Create(query.MessageId));

            if(getMessageResult.IsError)
            {
                return getMessageResult.Errors;
            }

            return new GetMessageResult(getMessageResult.Value);
        }
    }
}
