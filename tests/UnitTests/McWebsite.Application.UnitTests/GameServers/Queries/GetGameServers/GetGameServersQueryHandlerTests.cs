using FluentAssertions;
using McWebsite.Application.GameServers.Queries.GetGameServers;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.GameServers.TestUtils;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServers.Queries.GetGameServers
{
    public sealed class GetGameServersQueryHandlerTests
    {
        private readonly GetGameServersQueryHandler _handler;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _testEnvironment;
        public GetGameServersQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();
            _handler = new GetGameServersQueryHandler(_testEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidFilterParametersGetGameServersQueries))]
        public async Task HandleGetGameServersQuery_ValidQueryGiven_ShouldReturnGameServersList(GetGameServersQuery query)
        {
            // Arrange
            var validator = new GetGameServersQueryValidator();
            var existingGameServersCount = _testEnvironment.GameServers.Count();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.GameServers.Should().NotBeNull();
            queryResult.Value.GameServers.Should().NotBeEmpty();
            queryResult.Value.GameServers.Count().Should().Be(existingGameServersCount);
            _testEnvironment.MockGameServerRepository.Verify(x => x.GetGameServers(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(InvalidPageParameterGetGameServersQueries))]
        public async Task HandleGetGameServersQuery_InvalidPageParameterQueryGiven_ShouldBeCatchedByValidator(GetGameServersQuery query)
        {
            // Arrange
            var validator = new GetGameServersQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerRepository.Verify(x => x.GetGameServers(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(InvalidEntriesPerPageParameterGetGameServersQueries))]
        public async Task HandleGetGameServersQuery_InvalidEntriesPerPageParameterQueryGiven_ShouldBeCatchedByValidator(GetGameServersQuery query)
        {
            // Arrange
            var validator = new GetGameServersQueryValidator();

            // Act & Assert
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Count.Should().Be(1);
            _testEnvironment.MockGameServerRepository.Verify(x => x.GetGameServers(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        public static IEnumerable<object[]> ValidFilterParametersGetGameServersQueries()
        {
            yield return new object[] { GetGameServersQueryUtils.Create() };
            yield return new object[] { GetGameServersQueryUtils.Create() };
            yield return new object[] { GetGameServersQueryUtils.Create() };
        }
        public static IEnumerable<object[]> InvalidPageParameterGetGameServersQueries()
        {
            yield return new object[] { GetGameServersQueryUtils.Create(page: -40) };
            yield return new object[] { GetGameServersQueryUtils.Create(page: -20) };
            yield return new object[] { GetGameServersQueryUtils.Create(page: -10) };
        }
        public static IEnumerable<object[]> InvalidEntriesPerPageParameterGetGameServersQueries()
        {
            yield return new object[] { GetGameServersQueryUtils.Create(entriesPerPage: 0) };
            yield return new object[] { GetGameServersQueryUtils.Create(entriesPerPage: -10) };
            yield return new object[] { GetGameServersQueryUtils.Create(entriesPerPage: 4) };
        }


    }
}
