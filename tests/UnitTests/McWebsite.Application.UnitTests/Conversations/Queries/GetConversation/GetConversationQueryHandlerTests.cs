using FluentAssertions;
using McWebsite.Application.Conversations.Queries.GetConversationQuery;
using McWebsite.Application.UnitTests.Conversations.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.Conversation.ValueObjects;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.Conversations.Queries.GetConversation
{
    public sealed class GetConversationQueryHandlerTests
    {
        private readonly GetConversationQueryHandler _handler;
        private readonly UnitTestEnvironments.ConversationTestEnvironment _conversationTestEnvironment;
        private readonly UnitTestEnvironments.MessageTestEnvironment _messageTestEnvironment;
        public GetConversationQueryHandlerTests()
        {
            _conversationTestEnvironment = UnitTestEnvironments.ConversationTestEnvironment.Create();
            _messageTestEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();

            _handler = new GetConversationQueryHandler(_conversationTestEnvironment.MockConversationRepository.Object,
                                                       _messageTestEnvironment.MockMessageRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdGetConversationQueries))]
        public async Task HandleGetConversationQuery_ValidQueryGiven_ShouldReturnConversation(GetConversationQuery query)
        {
            // Arrange
            var validator = new GetConversationQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.Conversation.Should().NotBeNull();
            queryResult.Value.Conversation.Id.Value.Should().Be(query.ConversationId);
            _conversationTestEnvironment.MockConversationRepository.Verify(x => x.GetConversation(It.IsAny<ConversationId>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingIdGetConversationQueries))]
        public async Task HandleGetConversationQuery_InvalidNotExistingIdQueryGiven_ShouldReturnNotFoundError(GetConversationQuery query)
        {
            // Arrange
            var validator = new GetConversationQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeTrue();
            queryResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _conversationTestEnvironment.MockConversationRepository.Verify(x => x.GetConversation(It.IsAny<ConversationId>()), Times.Once);
        }

        public static IEnumerable<object[]> ValidIdGetConversationQueries()
        {
            var testEnvironment = UnitTestEnvironments.ConversationTestEnvironment.Create();

            yield return new object[] { GetConversationQueryUtils.Create()};
            yield return new object[] { GetConversationQueryUtils.Create(testEnvironment.Conversations[1].Id.Value)};
        }
        public static IEnumerable<object[]> InvalidNotExistingIdGetConversationQueries()
        {
            yield return new object[] { GetConversationQueryUtils.Create(Guid.NewGuid())};
            yield return new object[] { GetConversationQueryUtils.Create(Guid.NewGuid())};
        }
    }
}
