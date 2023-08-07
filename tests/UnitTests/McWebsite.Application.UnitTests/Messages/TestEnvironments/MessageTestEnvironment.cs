using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.Message.Entities;
using McWebsite.Domain.MessageModel.ValueObjects;
using Moq;

namespace McWebsite.Application.UnitTests.TestEnvironments
{
    public static partial class UnitTestEnvironments
    {
        private static List<Message> Messages = new List<Message>
            {
                Message.Recreate(Constants.MessageQueriesAndCommands.Id,
                                 Constants.ConversationQueriesAndCommands.Id,
                                 Constants.MessageQueriesAndCommands.ReceiverId,
                                 Constants.MessageQueriesAndCommands.ShipperId,
                                 Constants.MessageQueriesAndCommands.MessageContent,
                                 DateTime.UtcNow,
                                 DateTime.UtcNow),
                Message.Create(Constants.ConversationQueriesAndCommands.Id,
                               Constants.MessageQueriesAndCommands.ReceiverId,
                               Constants.MessageQueriesAndCommands.ShipperId,
                               Constants.MessageQueriesAndCommands.MessageContent,
                               DateTime.UtcNow,
                               DateTime.UtcNow)
            };
        public class MessageTestEnvironment
        {
            public List<Message> Messages = null!;
            public Mock<IMessageRepository> MockMessageRepository = null!;

            private MessageTestEnvironment()
            {
            }

            public static MessageTestEnvironment Create()
            {
                MessageTestEnvironment testEnvironment = new MessageTestEnvironment();

                testEnvironment.Messages = new List<Message>(UnitTestEnvironments.Messages);
                testEnvironment.MockMessageRepository = GetMock(testEnvironment.Messages);

                return testEnvironment;
            }

            public static Mock<IMessageRepository> GetMock(List<Message> testCollection)
            {
                Mock<IMessageRepository> mock = new Mock<IMessageRepository>();

                mock.Setup(m => m.CreateMessage(It.IsAny<Message>()))
                    .ReturnsAsync((Message message) =>
                    {
                        testCollection.Add(message);
                        return message;
                    });

                mock.Setup(m => m.GetMessage(It.IsAny<MessageId>())).ReturnsAsync((MessageId Id)
                    =>
                {
                    if (testCollection.FirstOrDefault(msg => msg.Id.Value == Id.Value) is not Message foundMessage)
                    {
                        return Errors.DomainModels.ModelNotFound;
                    }

                    return foundMessage;
                });

                mock.Setup(m => m.GetMessages(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync((int page, int entriesPerPage) =>
                    testCollection.OrderByDescending(p => p.SentDateTime)
                        .Skip(page)
                        .Take(entriesPerPage)
                        .ToList());

                mock.Setup(m => m.DeleteMessage(It.IsAny<Message>()))
                    .Returns((Message message) =>
                    {
                        testCollection.RemoveAll(msgEntry => msgEntry.Id.Value == message.Id.Value);
                        return Task.CompletedTask;
                    });

                mock.Setup(m => m.UpdateMessage(It.IsAny<Message>()))
                    .ReturnsAsync((Message updatedMessage) =>
                    {
                        int messageIndex = testCollection.FindIndex(msg => msg.Id.Value == updatedMessage.Id.Value);


                        testCollection[messageIndex] = Message.Recreate(updatedMessage.Id.Value,
                                                                         updatedMessage.ConversationId.Value,
                                                                         updatedMessage.ReceiverId.Value,
                                                                         updatedMessage.ShipperId.Value,
                                                                         updatedMessage.MessageContent,
                                                                         updatedMessage.SentDateTime,
                                                                         updatedMessage.UpdatedDateTime);
                        return updatedMessage;
                    });

                return mock;
            }
        }
    }
}
