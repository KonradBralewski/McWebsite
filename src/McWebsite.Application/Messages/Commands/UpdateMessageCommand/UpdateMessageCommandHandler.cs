using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;
using McWebsite.Domain.MessageModel.ValueObjects;
using McWebsite.Domain.Message.Entities;

namespace McWebsite.Application.Messages.Commands.UpdateMessageCommand
{
    public sealed class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, ErrorOr<UpdateMessageResult>?>
    {
        private readonly IMessageRepository _messageRepository;
        public UpdateMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<ErrorOr<UpdateMessageResult>?> Handle(UpdateMessageCommand command, CancellationToken cancellationToken)
        {
            var messageSearchResult = await _messageRepository.GetMessage(MessageId.Create(command.MessageId));

            if (messageSearchResult.IsError)
            {
                return messageSearchResult.Errors;
            }

            Message foundMessage = messageSearchResult.Value;

            if(ApplyModfications(foundMessage, command) is not Message messageAfterUpdate)
            {
                return null;
            }

            messageAfterUpdate.Update();

            var updatedMessageResult = await _messageRepository.UpdateMessage(messageAfterUpdate);

            if (updatedMessageResult.IsError)
            {
                return updatedMessageResult.Errors;
            }

            Message updatedMessage = updatedMessageResult.Value;

            return new UpdateMessageResult(updatedMessage);
        }

        private Message? ApplyModfications(Message toBeUpdated, UpdateMessageCommand command)
        {
            if(toBeUpdated.MessageContent == command.MessageContent)
            {
                return null;
            }

            return Message.Recreate(toBeUpdated.Id.Value,
                                       toBeUpdated.ConversationId.Value,
                                       toBeUpdated.ReceiverId.Value,
                                       toBeUpdated.ShipperId.Value,
                                       command.MessageContent,
                                       toBeUpdated.SentDateTime,
                                       DateTime.UtcNow);
        }
    }
}
