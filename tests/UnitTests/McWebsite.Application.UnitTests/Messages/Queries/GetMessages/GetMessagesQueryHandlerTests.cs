using FluentAssertions;
using McWebsite.Application.Messages.Queries.GetMessagesQuery;
using McWebsite.Application.UnitTests.Messages.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.Messages.Queries.GetMessages
{
    public sealed class GetMessagesQueryHandlerTests
    {
        private readonly GetMessagesQueryHandler _handler;
        private readonly UnitTestEnvironments.MessageTestEnvironment _testEnvironment;
        public GetMessagesQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();
            _handler = new GetMessagesQueryHandler(_testEnvironment.MockMessageRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidFilterParametersGetMessagesQueries))]
        public async Task HandleGetMessagesQuery_ValidQueryGiven_ShouldReturnMessageList(GetMessagesQuery query)
        {
            // Arrange
            var validator = new GetMessagesQueryValidator();
            var existingMessagesCount = _testEnvironment.Messages.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.Messages.Should().NotBeNull();
            queryResult.Value.Messages.Should().NotBeEmpty();
            queryResult.Value.Messages.Count().Should().Be(existingMessagesCount);
            _testEnvironment.MockMessageRepository.Verify(x => x.GetMessages(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidPageParameterGetMessagesQueries))]
        public async Task HandleGetMessagesQuery_InvalidPageParameterQueryGiven_ShouldBeCatchedByValidator(GetMessagesQuery query)
        {
            // Arrange
            var validator = new GetMessagesQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockMessageRepository.Verify(x => x.GetMessages(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEntriesPerPageParameterGetMessagesQueries))]
        public async Task HandleGetMessagesQuery_InvalidEntriesPerPageParameterQueryGiven_ShouldBeCatchedByValidator(GetMessagesQuery query)
        {
            // Arrange
            var validator = new GetMessagesQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockMessageRepository.Verify(x => x.GetMessages(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidFilterParametersGetMessagesQueries()
        {
            yield return new object[] { GetMessagesQueryUtils.Create() };
            yield return new object[] { GetMessagesQueryUtils.Create() };
            yield return new object[] { GetMessagesQueryUtils.Create() };
        }
        public static IEnumerable<object[]> InvalidPageParameterGetMessagesQueries()
        {
            yield return new object[] { GetMessagesQueryUtils.Create(page: -40) };
            yield return new object[] { GetMessagesQueryUtils.Create(page: -20) };
            yield return new object[] { GetMessagesQueryUtils.Create(page: -10) };
        }
        public static IEnumerable<object[]> InvalidEntriesPerPageParameterGetMessagesQueries()
        {
            yield return new object[] { GetMessagesQueryUtils.Create(entriesPerPage: 0) };
            yield return new object[] { GetMessagesQueryUtils.Create(entriesPerPage: -10) };
            yield return new object[] { GetMessagesQueryUtils.Create(entriesPerPage: 4) };
        }


    }
}
