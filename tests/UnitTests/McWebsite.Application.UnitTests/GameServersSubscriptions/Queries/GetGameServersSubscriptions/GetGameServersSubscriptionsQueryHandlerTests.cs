using FluentAssertions;
using McWebsite.Application.GameServerSubscriptions.Queries.GetGameServersSubscriptionsQuery;
using McWebsite.Application.UnitTests.GameServersSubscriptions.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServersSubscriptions.Queries.GetGameServersSubscriptions
{
    public sealed class GetGameServersSubscriptionsQueryHandlerTests
    {
        private readonly GetGameServersSubscriptionsQueryHandler _handler;
        private readonly UnitTestEnvironments.GameServerSubscriptionTestEnvironment _testEnvironment;
        public GetGameServersSubscriptionsQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerSubscriptionTestEnvironment.Create();
            _handler = new GetGameServersSubscriptionsQueryHandler(_testEnvironment.MockGameServerSubscriptionRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidFilterParametersGetGameServersSubscriptionsQueries))]
        public async Task HandleGetGameServersSubscriptionsQuery_ValidQueryGiven_ShouldReturnGameServersSubscriptionList(GetGameServersSubscriptionsQuery query)
        {
            // Arrange
            var validator = new GetGameServersSubscriptionsQueryValidator();
            var existingGameServersSubscriptionsCount = _testEnvironment.GameServersSubscriptions.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.GameServersSubscriptions.Should().NotBeNull();
            queryResult.Value.GameServersSubscriptions.Should().NotBeEmpty();
            queryResult.Value.GameServersSubscriptions.Count().Should().Be(existingGameServersSubscriptionsCount);
            _testEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.GetGameServersSubscriptions(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidPageParameterGetGameServersSubscriptionsQueries))]
        public async Task HandleGetGameServersSubscriptionsQuery_InvalidPageParameterQueryGiven_ShouldBeCatchedByValidator(GetGameServersSubscriptionsQuery query)
        {
            // Arrange
            var validator = new GetGameServersSubscriptionsQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.GetGameServersSubscriptions(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEntriesPerPageParameterGetGameServersSubscriptionsQueries))]
        public async Task HandleGetGameServersSubscriptionsQuery_InvalidEntriesPerPageParameterQueryGiven_ShouldBeCatchedByValidator(GetGameServersSubscriptionsQuery query)
        {
            // Arrange
            var validator = new GetGameServersSubscriptionsQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerSubscriptionRepository.Verify(x => x.GetGameServersSubscriptions(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidFilterParametersGetGameServersSubscriptionsQueries()
        {
            yield return new object[] { GetGameServersSubscriptionsQueryUtils.Create() };
            yield return new object[] { GetGameServersSubscriptionsQueryUtils.Create() };
            yield return new object[] { GetGameServersSubscriptionsQueryUtils.Create() };
        }
        public static IEnumerable<object[]> InvalidPageParameterGetGameServersSubscriptionsQueries()
        {
            yield return new object[] { GetGameServersSubscriptionsQueryUtils.Create(page: -40) };
            yield return new object[] { GetGameServersSubscriptionsQueryUtils.Create(page: -20) };
            yield return new object[] { GetGameServersSubscriptionsQueryUtils.Create(page: -10) };
        }
        public static IEnumerable<object[]> InvalidEntriesPerPageParameterGetGameServersSubscriptionsQueries()
        {
            yield return new object[] { GetGameServersSubscriptionsQueryUtils.Create(entriesPerPage: 0) };
            yield return new object[] { GetGameServersSubscriptionsQueryUtils.Create(entriesPerPage: -10) };
            yield return new object[] { GetGameServersSubscriptionsQueryUtils.Create(entriesPerPage: 4) };
        }


    }
}
