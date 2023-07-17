using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.MessageModel.ValueObjects;
using MediatR;

namespace McWebsite.Application.Messages.Commands.DeleteMessageCommand
{
    public sealed class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, ErrorOr<bool>>
    {
        private readonly IMessageRepository _messageRepository;
        public DeleteMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var messageSearchResult = await _messageRepository.GetMessage(MessageId.Create(request.MessageId));

            if(messageSearchResult.IsError)
            {
                return messageSearchResult.Errors;
            }

            Message message = messageSearchResult.Value;

            message.Delete();

            await _messageRepository.DeleteMessage(message);

            return true;
        }
    }
}
