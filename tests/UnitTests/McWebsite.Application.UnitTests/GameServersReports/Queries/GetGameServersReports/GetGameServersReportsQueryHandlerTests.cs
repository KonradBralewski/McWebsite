using FluentAssertions;
using McWebsite.Application.GameServerReports.Queries.GetGameServersReportsQuery;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.GameServersReports.TestUtils;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServersReports.Queries.GetGameServersReports
{
    public sealed class GetGameServersReportsQueryHandlerTests
    {
        private readonly GetGameServersReportsQueryHandler _handler;
        private readonly UnitTestEnvironments.GameServerReportTestEnvironment _testEnvironment;
        public GetGameServersReportsQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerReportTestEnvironment.Create();
            _handler = new GetGameServersReportsQueryHandler(_testEnvironment.MockGameServerReportRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidFilterParametersGetGameServersReportsQueries))]
        public async Task HandleGetGameServersReportsQuery_ValidQueryGiven_ShouldReturnGameServersReportList(GetGameServersReportsQuery query)
        {
            // Arrange
            var validator = new GetGameServersReportsQueryValidator();
            var existingGameServersCount = _testEnvironment.GameServersReports.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.GameServersReports.Should().NotBeNull();
            queryResult.Value.GameServersReports.Should().NotBeEmpty();
            queryResult.Value.GameServersReports.Count().Should().Be(existingGameServersCount);
            _testEnvironment.MockGameServerReportRepository.Verify(x => x.GetGameServersReports(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidPageParameterGetGameServersReportsQueries))]
        public async Task HandleGetGameServersReportsQuery_InvalidPageParameterQueryGiven_ShouldBeCatchedByValidator(GetGameServersReportsQuery query)
        {
            // Arrange
            var validator = new GetGameServersReportsQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerReportRepository.Verify(x => x.GetGameServersReports(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEntriesPerPageParameterGetGameServersReportsQueries))]
        public async Task HandleGetGameServersReportsQuery_InvalidEntriesPerPageParameterQueryGiven_ShouldBeCatchedByValidator(GetGameServersReportsQuery query)
        {
            // Arrange
            var validator = new GetGameServersReportsQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerReportRepository.Verify(x => x.GetGameServersReports(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidFilterParametersGetGameServersReportsQueries()
        {
            yield return new object[] { GetGameServersReportsQueryUtils.Create() };
            yield return new object[] { GetGameServersReportsQueryUtils.Create() };
            yield return new object[] { GetGameServersReportsQueryUtils.Create() };
        }
        public static IEnumerable<object[]> InvalidPageParameterGetGameServersReportsQueries()
        {
            yield return new object[] { GetGameServersReportsQueryUtils.Create(page: -40) };
            yield return new object[] { GetGameServersReportsQueryUtils.Create(page: -20) };
            yield return new object[] { GetGameServersReportsQueryUtils.Create(page: -10) };
        }
        public static IEnumerable<object[]> InvalidEntriesPerPageParameterGetGameServersReportsQueries()
        {
            yield return new object[] { GetGameServersReportsQueryUtils.Create(entriesPerPage: 0) };
            yield return new object[] { GetGameServersReportsQueryUtils.Create(entriesPerPage: -10) };
            yield return new object[] { GetGameServersReportsQueryUtils.Create(entriesPerPage: 4) };
        }


    }
}
