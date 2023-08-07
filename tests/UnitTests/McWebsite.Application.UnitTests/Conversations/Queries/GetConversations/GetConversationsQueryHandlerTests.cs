using FluentAssertions;
using McWebsite.Application.Conversations.Queries.GetConversationsQuery;
using McWebsite.Application.UnitTests.Conversations.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.Conversations.Queries.GetConversations
{
    public sealed class GetConversationsQueryHandlerTests
    {
        private readonly GetConversationsQueryHandler _handler;
        private readonly UnitTestEnvironments.ConversationTestEnvironment _testEnvironment;
        public GetConversationsQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.ConversationTestEnvironment.Create();
            _handler = new GetConversationsQueryHandler(_testEnvironment.MockConversationRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidFilterParametersGetConversationsQueries))]
        public async Task HandleGetConversationsQuery_ValidQueryGiven_ShouldReturnConversationList(GetConversationsQuery query)
        {
            // Arrange
            var validator = new GetConversationsQueryValidator();
            var existingConversationsCount = _testEnvironment.Conversations.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.Conversations.Should().NotBeNull();
            queryResult.Value.Conversations.Should().NotBeEmpty();
            queryResult.Value.Conversations.Count().Should().Be(existingConversationsCount);
            _testEnvironment.MockConversationRepository.Verify(x => x.GetConversations(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidPageParameterGetConversationsQueries))]
        public async Task HandleGetConversationsQuery_InvalidPageParameterQueryGiven_ShouldBeCatchedByValidator(GetConversationsQuery query)
        {
            // Arrange
            var validator = new GetConversationsQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockConversationRepository.Verify(x => x.GetConversations(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEntriesPerPageParameterGetConversationsQueries))]
        public async Task HandleGetConversationsQuery_InvalidEntriesPerPageParameterQueryGiven_ShouldBeCatchedByValidator(GetConversationsQuery query)
        {
            // Arrange
            var validator = new GetConversationsQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockConversationRepository.Verify(x => x.GetConversations(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidFilterParametersGetConversationsQueries()
        {
            yield return new object[] { GetConversationsQueryUtils.Create() };
            yield return new object[] { GetConversationsQueryUtils.Create() };
            yield return new object[] { GetConversationsQueryUtils.Create() };
        }
        public static IEnumerable<object[]> InvalidPageParameterGetConversationsQueries()
        {
            yield return new object[] { GetConversationsQueryUtils.Create(page: -40) };
            yield return new object[] { GetConversationsQueryUtils.Create(page: -20) };
            yield return new object[] { GetConversationsQueryUtils.Create(page: -10) };
        }
        public static IEnumerable<object[]> InvalidEntriesPerPageParameterGetConversationsQueries()
        {
            yield return new object[] { GetConversationsQueryUtils.Create(entriesPerPage: 0) };
            yield return new object[] { GetConversationsQueryUtils.Create(entriesPerPage: -10) };
            yield return new object[] { GetConversationsQueryUtils.Create(entriesPerPage: 4) };
        }


    }
}
