using FluentAssertions;
using McWebsite.Application.GameServerSubscriptions.Queries.GetGameServerSubscriptionQuery;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using Moq;
using Xunit;
using McWebsite.Application.UnitTests.GameServersSubscriptions.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;

namespace McWebsite.Application.UnitTests.GameServersSubscriptions.Queries.GetGameServerSubscription
{
    public sealed class GetGameServerSubscriptionQueryHandlerTests
    {
        private readonly GetGameServerSubscriptionQueryHandler _handler;
        private readonly UnitTestEnvironments.GameServerSubscriptionTestEnvironment _testEnvironment;
        public GetGameServerSubscriptionQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerSubscriptionTestEnvironment.Create();
            _handler = new GetGameServerSubscriptionQueryHandler(_testEnvironment.MockGameServerSubscriptionRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdGetGameServerSubscriptionQueries))]
        public async Task HandleGetGameServerSubscriptionQuery_ValidQueryGiven_ShouldReturnGameServerSubscription(GetGameServerSubscriptionQuery query)
        {
            // Arrange
            var validator = new GetGameServerSubscriptionQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.GameServerSubscription.Should().NotBeNull();
            queryResult.Value.GameServerSubscription.Id.Value.Should().Be(query.GameServerSubscriptionId);
            _testEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.GetGameServerSubscription(It.IsAny<GameServerSubscriptionId>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingIdGetGameServerSubscriptionQueries))]
        public async Task HandleGetGameServerSubscriptionQuery_InvalidNotExistingIdQueryGiven_ShouldReturnNotFoundError(GetGameServerSubscriptionQuery query)
        {
            // Arrange
            var validator = new GetGameServerSubscriptionQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeTrue();
            queryResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.GetGameServerSubscription(It.IsAny<GameServerSubscriptionId>()), Times.Once);
        }

        public static IEnumerable<object[]> ValidIdGetGameServerSubscriptionQueries()
        {
            var testEnvironment = UnitTestEnvironments.GameServerSubscriptionTestEnvironment.Create();

            yield return new object[] { GetGameServerSubscriptionQueryUtils.Create() };
            yield return new object[] { GetGameServerSubscriptionQueryUtils.Create(testEnvironment.GameServersSubscriptions[1].Id.Value) };
        }
        public static IEnumerable<object[]> InvalidNotExistingIdGetGameServerSubscriptionQueries()
        {
            yield return new object[] { GetGameServerSubscriptionQueryUtils.Create(Guid.NewGuid()) };
            yield return new object[] { GetGameServerSubscriptionQueryUtils.Create(Guid.NewGuid()) };
        }
    }
}
