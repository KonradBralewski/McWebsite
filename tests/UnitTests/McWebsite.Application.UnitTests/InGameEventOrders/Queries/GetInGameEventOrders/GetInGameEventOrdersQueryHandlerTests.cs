using FluentAssertions;
using McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrdersQuery;
using McWebsite.Application.UnitTests.InGameEventOrders.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.InGameEventOrders.Queries.GetInGameEventOrders
{
    public sealed class GetInGameEventOrdersQueryHandlerTests
    {
        private readonly GetInGameEventOrdersQueryHandler _handler;
        private readonly UnitTestEnvironments.InGameEventOrderTestEnvironment _testEnvironment;
        public GetInGameEventOrdersQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.InGameEventOrderTestEnvironment.Create();
            _handler = new GetInGameEventOrdersQueryHandler(_testEnvironment.MockInGameEventOrderRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidFilterParametersGetInGameEventOrdersQueries))]
        public async Task HandleGetInGameEventOrdersQuery_ValidQueryGiven_ShouldReturnInGameEventOrderList(GetInGameEventOrdersQuery query)
        {
            // Arrange
            var validator = new GetInGameEventOrdersQueryValidator();
            var existingInGameEventOrdersCount = _testEnvironment.InGameEventOrders.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.InGameEventOrders.Should().NotBeNull();
            queryResult.Value.InGameEventOrders.Should().NotBeEmpty();
            queryResult.Value.InGameEventOrders.Count().Should().Be(existingInGameEventOrdersCount);
            _testEnvironment.MockInGameEventOrderRepository.Verify(x => x.GetInGameEventOrders(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidPageParameterGetInGameEventOrdersQueries))]
        public async Task HandleGetInGameEventOrdersQuery_InvalidPageParameterQueryGiven_ShouldBeCatchedByValidator(GetInGameEventOrdersQuery query)
        {
            // Arrange
            var validator = new GetInGameEventOrdersQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockInGameEventOrderRepository.Verify(x => x.GetInGameEventOrders(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEntriesPerPageParameterGetInGameEventOrdersQueries))]
        public async Task HandleGetInGameEventOrdersQuery_InvalidEntriesPerPageParameterQueryGiven_ShouldBeCatchedByValidator(GetInGameEventOrdersQuery query)
        {
            // Arrange
            var validator = new GetInGameEventOrdersQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockInGameEventOrderRepository.Verify(x => x.GetInGameEventOrders(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidFilterParametersGetInGameEventOrdersQueries()
        {
            yield return new object[] { GetInGameEventOrdersQueryUtils.Create() };
            yield return new object[] { GetInGameEventOrdersQueryUtils.Create() };
            yield return new object[] { GetInGameEventOrdersQueryUtils.Create() };
        }
        public static IEnumerable<object[]> InvalidPageParameterGetInGameEventOrdersQueries()
        {
            yield return new object[] { GetInGameEventOrdersQueryUtils.Create(page: -40) };
            yield return new object[] { GetInGameEventOrdersQueryUtils.Create(page: -20) };
            yield return new object[] { GetInGameEventOrdersQueryUtils.Create(page: -10) };
        }
        public static IEnumerable<object[]> InvalidEntriesPerPageParameterGetInGameEventOrdersQueries()
        {
            yield return new object[] { GetInGameEventOrdersQueryUtils.Create(entriesPerPage: 0) };
            yield return new object[] { GetInGameEventOrdersQueryUtils.Create(entriesPerPage: -10) };
            yield return new object[] { GetInGameEventOrdersQueryUtils.Create(entriesPerPage: 4) };
        }


    }
}
