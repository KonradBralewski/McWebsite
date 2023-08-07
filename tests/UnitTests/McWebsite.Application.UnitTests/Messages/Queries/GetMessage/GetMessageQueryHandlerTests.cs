using FluentAssertions;
using McWebsite.Application.Messages.Queries.GetMessageQuery;
using McWebsite.Application.UnitTests.Messages.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.MessageModel.ValueObjects;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.Messages.Queries.GetMessage
{
    public sealed class GetMessageQueryHandlerTests
    {
        private readonly GetMessageQueryHandler _handler;
        private readonly UnitTestEnvironments.MessageTestEnvironment _testEnvironment;
        public GetMessageQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();
            _handler = new GetMessageQueryHandler(_testEnvironment.MockMessageRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdGetMessageQueries))]
        public async Task HandleGetMessageQuery_ValidQueryGiven_ShouldReturnMessage(GetMessageQuery query)
        {
            // Arrange
            var validator = new GetMessageQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.Message.Should().NotBeNull();
            queryResult.Value.Message.Id.Value.Should().Be(query.MessageId);
            _testEnvironment.MockMessageRepository.Verify(x => x.GetMessage(It.IsAny<MessageId>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingIdGetMessageQueries))]
        public async Task HandleGetMessageQuery_InvalidNotExistingIdQueryGiven_ShouldReturnNotFoundError(GetMessageQuery query)
        {
            // Arrange
            var validator = new GetMessageQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeTrue();
            queryResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockMessageRepository.Verify(x => x.GetMessage(It.IsAny<MessageId>()), Times.Once);
        }

        public static IEnumerable<object[]> ValidIdGetMessageQueries()
        {
            var testEnvironment = UnitTestEnvironments.MessageTestEnvironment.Create();

            yield return new object[] { GetMessageQueryUtils.Create()};
            yield return new object[] { GetMessageQueryUtils.Create(testEnvironment.Messages[1].Id.Value)};
        }
        public static IEnumerable<object[]> InvalidNotExistingIdGetMessageQueries()
        {
            yield return new object[] { GetMessageQueryUtils.Create(Guid.NewGuid())};
            yield return new object[] { GetMessageQueryUtils.Create(Guid.NewGuid())};
        }
    }
}
