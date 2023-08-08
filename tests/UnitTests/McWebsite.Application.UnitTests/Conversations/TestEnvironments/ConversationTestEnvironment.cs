using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.Conversation.ValueObjects;
using McWebsite.Domain.User.ValueObjects;
using Moq;

namespace McWebsite.Application.UnitTests.TestEnvironments
{
    public static partial class UnitTestEnvironments
    {
        private static List<Conversation> Conversations = new List<Conversation>
            {
                Conversation.Recreate(Constants.ConversationQueriesAndCommands.Id,
                                    Constants.ConversationQueriesAndCommands.FirstParticipant,
                                    Constants.ConversationQueriesAndCommands.SecondParticipant,
                                    Constants.ConversationQueriesAndCommands.MessagesIds,
                                    DateTime.UtcNow,
                                    DateTime.UtcNow),
                Conversation.Create(Constants.ConversationQueriesAndCommands.FirstParticipant,
                                    Constants.ConversationQueriesAndCommands.SecondParticipant,
                                    DateTime.UtcNow,
                                    DateTime.UtcNow)
            };
        public class ConversationTestEnvironment
        {
            public List<Conversation> Conversations = null!;
            public Mock<IConversationRepository> MockConversationRepository = null!;

            private ConversationTestEnvironment()
            {
            }

            public static ConversationTestEnvironment Create()
            {
                ConversationTestEnvironment testEnvironment = new ConversationTestEnvironment();

                testEnvironment.Conversations = new List<Conversation>(UnitTestEnvironments.Conversations);
                testEnvironment.MockConversationRepository = GetMock(testEnvironment.Conversations);

                return testEnvironment;
            }

            public static Mock<IConversationRepository> GetMock(List<Conversation> testCollection)
            {
                Mock<IConversationRepository> mock = new Mock<IConversationRepository>();

                mock.Setup(m => m.CreateConversation(It.IsAny<Conversation>()))
                    .ReturnsAsync((Conversation conversation) =>
                    {
                        testCollection.Add(conversation);
                        return conversation;
                    });

                mock.Setup(m => m.GetConversation(It.IsAny<ConversationId>())).ReturnsAsync((ConversationId Id)
                    =>
                {
                    if (testCollection.FirstOrDefault(c => c.Id.Value == Id.Value) is not Conversation foundConversation)
                    {
                        return Errors.DomainModels.ModelNotFound;
                    }

                    return foundConversation;
                });

                mock.Setup(m => m.GetConversation(It.IsAny<UserId>(), It.IsAny<UserId>()))
                    .ReturnsAsync((UserId firstParticipantId, UserId secondParticipantId) =>
                    {
                        var foundConversation = testCollection.FirstOrDefault(c => c.Participants.FirstParticipantId.Value == firstParticipantId.Value
                            && c.Participants.SecondParticipantId.Value == secondParticipantId.Value);

                        if (foundConversation is not null)
                        {
                            return foundConversation;
                        }

                        foundConversation = testCollection.FirstOrDefault(c => c.Participants.SecondParticipantId.Value == firstParticipantId.Value
                            && c.Participants.FirstParticipantId.Value == secondParticipantId.Value);

                        if (foundConversation is not null)
                        {
                            return foundConversation;
                        }

                        return Errors.DomainModels.ModelNotFound;
                    });

                mock.Setup(m => m.GetConversations(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync((int page, int entriesPerPage) =>
                    testCollection.OrderByDescending(p => p.CreatedDateTime)
                        .Skip(page)
                        .Take(entriesPerPage)
                        .ToList());

                mock.Setup(m => m.DeleteConversation(It.IsAny<Conversation>()))
                    .Returns((Conversation conversation) =>
                    {
                        testCollection.RemoveAll(cEntry => cEntry.Id.Value == conversation.Id.Value);
                        return Task.CompletedTask;
                    });


                return mock;
            }



        }
    }
}
