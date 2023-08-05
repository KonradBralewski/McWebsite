using FluentAssertions;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventQuery;
using McWebsite.Application.UnitTests.InGameEvents.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.InGameEvent.ValueObjects;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.InGameEvents.Queries.GetInGameEvent
{
    public sealed class GetInGameEventQueryHandlerTests
    {
        private readonly GetInGameEventQueryHandler _handler;
        private readonly UnitTestEnvironments.InGameEventTestEnvironment _testEnvironment;
        public GetInGameEventQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();
            _handler = new GetInGameEventQueryHandler(_testEnvironment.MockInGameEventRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdGetInGameEventQueries))]
        public async Task HandleGetInGameEventQuery_ValidQueryGiven_ShouldReturnInGameEvent(GetInGameEventQuery query)
        {
            // Arrange
            var validator = new GetInGameEventQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.InGameEvent.Should().NotBeNull();
            queryResult.Value.InGameEvent.Id.Value.Should().Be(query.InGameEventId);
            _testEnvironment.MockInGameEventRepository.Verify(x => x.GetInGameEvent(It.IsAny<InGameEventId>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(BadIdsGetInGameEventQueries))]
        public async Task HandleGetInGameEventQuery_NotExistingIdQueryGiven_ShouldReturnNotFoundError(GetInGameEventQuery query)
        {
            // Arrange
            var validator = new GetInGameEventQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeTrue();
            queryResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockInGameEventRepository.Verify(x => x.GetInGameEvent(It.IsAny<InGameEventId>()), Times.Once);
        }

        public static IEnumerable<object[]> ValidIdGetInGameEventQueries()
        {
            var testEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();

            yield return new object[] { GetInGameEventQueryUtils.Create()};
            yield return new object[] { GetInGameEventQueryUtils.Create(testEnvironment.InGameEvents[1].Id.Value)};
        }
        public static IEnumerable<object[]> BadIdsGetInGameEventQueries()
        {
            yield return new object[] { GetInGameEventQueryUtils.Create(Guid.NewGuid())};
            yield return new object[] { GetInGameEventQueryUtils.Create(Guid.NewGuid())};
        }
    }
}
