using FluentAssertions;
using McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrderQuery;
using McWebsite.Application.UnitTests.InGameEventOrders.TestUtils;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Domain.InGameEventOrder.ValueObjects;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.InGameEventOrders.Queries.GetInGameEventOrder
{
    public sealed class GetInGameEventOrderQueryHandlerTests
    {
        private readonly GetInGameEventOrderQueryHandler _handler;
        private readonly UnitTestEnvironments.InGameEventOrderTestEnvironment _testEnvironment;
        public GetInGameEventOrderQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.InGameEventOrderTestEnvironment.Create();
            _handler = new GetInGameEventOrderQueryHandler(_testEnvironment.MockInGameEventOrderRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdGetInGameEventOrderQueries))]
        public async Task HandleGetInGameEventOrderQuery_ValidQueryGiven_ShouldReturnInGameEventOrder(GetInGameEventOrderQuery query)
        {
            // Arrange
            var validator = new GetInGameEventOrderQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.InGameEventOrder.Should().NotBeNull();
            queryResult.Value.InGameEventOrder.Id.Value.Should().Be(query.InGameEventOrderId);
            _testEnvironment.MockInGameEventOrderRepository.Verify(x => x.GetInGameEventOrder(It.IsAny<InGameEventOrderId>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(BadIdsGetInGameEventOrderQueries))]
        public async Task HandleGetInGameEventOrderQuery_NotExistingIdQueryGiven_ShouldReturnNotFoundError(GetInGameEventOrderQuery query)
        {
            // Arrange
            var validator = new GetInGameEventOrderQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeTrue();
            queryResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockInGameEventOrderRepository.Verify(x => x.GetInGameEventOrder(It.IsAny<InGameEventOrderId>()), Times.Once);
        }

        public static IEnumerable<object[]> ValidIdGetInGameEventOrderQueries()
        {
            var testEnvironment = UnitTestEnvironments.InGameEventOrderTestEnvironment.Create();

            yield return new object[] { GetInGameEventOrderQueryUtils.Create()};
            yield return new object[] { GetInGameEventOrderQueryUtils.Create(testEnvironment.InGameEventOrders[1].Id.Value)};
        }
        public static IEnumerable<object[]> BadIdsGetInGameEventOrderQueries()
        {
            yield return new object[] { GetInGameEventOrderQueryUtils.Create(Guid.NewGuid())};
            yield return new object[] { GetInGameEventOrderQueryUtils.Create(Guid.NewGuid())};
        }
    }
}
