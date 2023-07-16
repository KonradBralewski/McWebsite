using ErrorOr;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.MessageModel.ValueObjects;

namespace McWebsite.Application.Common.Interfaces.Persistence
{
    public interface IMessageRepository
    {
        Task<ErrorOr<IEnumerable<Message>>> GetMessages(int page, int entriesPerPage);
        Task<ErrorOr<Message>> GetMessage(MessageId messageId);
        Task<ErrorOr<Message>> CreateMessage(Message message);
        Task<ErrorOr<Message>> UpdateMessage(Message message);
        Task DeleteMessage(Message message);
    }
}
