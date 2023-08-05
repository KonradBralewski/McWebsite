using FluentAssertions;
using Moq;
using Xunit;
using McWebsite.Application.InGameEvents.Queries.GetInGameEventsQuery;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.InGameEvents.TestUtils;

namespace McWebsite.Application.UnitTests.InGameEvents.Queries.GetInGameEvents
{
    public sealed class GetInGameEventsQueryHandlerTests
    {
        private readonly GetInGameEventsQueryHandler _handler;
        private readonly UnitTestEnvironments.InGameEventTestEnvironment _testEnvironment;
        public GetInGameEventsQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.InGameEventTestEnvironment.Create();
            _handler = new GetInGameEventsQueryHandler(_testEnvironment.MockInGameEventRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidFilterParametersGetInGameEventsQueries))]
        public async Task HandleGetInGameEventsQuery_ValidQueryGiven_ShouldReturnInGameEventList(GetInGameEventsQuery query)
        {
            // Arrange
            var validator = new GetInGameEventsQueryValidator();
            var existingInGameEventsCount = _testEnvironment.InGameEvents.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.InGameEvents.Should().NotBeNull();
            queryResult.Value.InGameEvents.Should().NotBeEmpty();
            queryResult.Value.InGameEvents.Count().Should().Be(existingInGameEventsCount);
            _testEnvironment.MockInGameEventRepository.Verify(x => x.GetInGameEvents(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidPageParameterGetInGameEventsQueries))]
        public async Task HandleGetInGameEventsQuery_InvalidPageParameterQueryGiven_ShouldBeCatchedByValidator(GetInGameEventsQuery query)
        {
            // Arrange
            var validator = new GetInGameEventsQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockInGameEventRepository.Verify(x => x.GetInGameEvents(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEntriesPerPageParameterGetInGameEventsQueries))]
        public async Task HandleGetInGameEventsQuery_InvalidEntriesPerPageParameterQueryGiven_ShouldBeCatchedByValidator(GetInGameEventsQuery query)
        {
            // Arrange
            var validator = new GetInGameEventsQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockInGameEventRepository.Verify(x => x.GetInGameEvents(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidFilterParametersGetInGameEventsQueries()
        {
            yield return new object[] { GetInGameEventsQueryUtils.Create() };
            yield return new object[] { GetInGameEventsQueryUtils.Create() };
            yield return new object[] { GetInGameEventsQueryUtils.Create() };
        }
        public static IEnumerable<object[]> InvalidPageParameterGetInGameEventsQueries()
        {
            yield return new object[] { GetInGameEventsQueryUtils.Create(page: -40) };
            yield return new object[] { GetInGameEventsQueryUtils.Create(page: -20) };
            yield return new object[] { GetInGameEventsQueryUtils.Create(page: -10) };
        }
        public static IEnumerable<object[]> InvalidEntriesPerPageParameterGetInGameEventsQueries()
        {
            yield return new object[] { GetInGameEventsQueryUtils.Create(entriesPerPage: 0) };
            yield return new object[] { GetInGameEventsQueryUtils.Create(entriesPerPage: -10) };
            yield return new object[] { GetInGameEventsQueryUtils.Create(entriesPerPage: 4) };
        }


    }
}
