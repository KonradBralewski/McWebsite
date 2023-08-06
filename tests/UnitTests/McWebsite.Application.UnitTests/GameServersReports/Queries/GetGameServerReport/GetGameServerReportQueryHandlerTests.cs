using FluentAssertions;
using McWebsite.Application.GameServerReports.Queries.GetGameServerReportQuery;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.GameServersReports.TestUtils;
using McWebsite.Domain.GameServerReport.ValueObjects;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServersReports.Queries.GetGameServerReport
{
    public sealed class GetGameServerReportQueryHandlerTests
    {
        private readonly GetGameServerReportQueryHandler _handler;
        private readonly UnitTestEnvironments.GameServerReportTestEnvironment _testEnvironment;
        public GetGameServerReportQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerReportTestEnvironment.Create();
            _handler = new GetGameServerReportQueryHandler(_testEnvironment.MockGameServerReportRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdGetGameServerReportQueries))]
        public async Task HandleGetGameServerReportQuery_ValidQueryGiven_ShouldReturnGameServerReport(GetGameServerReportQuery query)
        {
            // Arrange
            var validator = new GetGameServerReportQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.GameServerReport.Should().NotBeNull();
            queryResult.Value.GameServerReport.Id.Value.Should().Be(query.GameServerReportId);
            _testEnvironment.MockGameServerReportRepository.Verify(x => x.GetGameServerReport(It.IsAny<GameServerReportId>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidNotExistingIdGetGameServerReportQueries))]
        public async Task HandleGetGameServerReportQuery_InvalidNotExistingIdQueryGiven_ShouldReturnNotFoundError(GetGameServerReportQuery query)
        {
            // Arrange
            var validator = new GetGameServerReportQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeTrue();
            queryResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockGameServerReportRepository.Verify(x => x.GetGameServerReport(It.IsAny<GameServerReportId>()), Times.Once);
        }

        public static IEnumerable<object[]> ValidIdGetGameServerReportQueries()
        {
            var testEnvironment = UnitTestEnvironments.GameServerReportTestEnvironment.Create();

            yield return new object[] { GetGameServerReportQueryUtils.Create() };
            yield return new object[] { GetGameServerReportQueryUtils.Create(testEnvironment.GameServersReports[1].Id.Value) };
        }
        public static IEnumerable<object[]> InvalidNotExistingIdGetGameServerReportQueries()
        {
            yield return new object[] { GetGameServerReportQueryUtils.Create(Guid.NewGuid()) };
            yield return new object[] { GetGameServerReportQueryUtils.Create(Guid.NewGuid()) };
        }
    }
}
