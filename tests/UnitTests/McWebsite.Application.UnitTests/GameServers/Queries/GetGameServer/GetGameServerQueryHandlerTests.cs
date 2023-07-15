using FluentAssertions;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.GameServers.Commands.CreateGameServerCommand;
using McWebsite.Application.GameServers.Queries.GetGameServer;
using McWebsite.Application.UnitTests.TestEnvironments;
using McWebsite.Application.UnitTests.GameServers.TestUtils;
using McWebsite.Application.UnitTests.TestUtils.Extensions;
using McWebsite.Domain.GameServer.ValueObjects;
using Moq;
using Xunit;

namespace McWebsite.Application.UnitTests.GameServers.Queries.GetGameServer
{
    public sealed class GetGameServerQueryHandlerTests
    {
        private readonly GetGameServerQueryHandler _handler;
        private readonly UnitTestEnvironments.GameServerTestEnvironment _testEnvironment;
        public GetGameServerQueryHandlerTests()
        {
            _testEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();
            _handler = new GetGameServerQueryHandler(_testEnvironment.MockGameServerRepository.Object);
        }

        [Theory]
        [MemberData(nameof(ValidIdsGetGameServerQueries))]
        public async Task HandleGetGameServerQuery_ValidQueryGiven_ShouldReturnGameServer(GetGameServerQuery query)
        {
            // Arrange
            var validator = new GetGameServerQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeFalse();
            queryResult.Value.GameServer.Should().NotBeNull();
            queryResult.Value.GameServer.Id.Value.Should().Be(query.GameServerId);
            _testEnvironment.MockGameServerRepository.Verify(x => x.GetGameServer(It.IsAny<GameServerId>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(BadIdsGetGameServerQueries))]
        public async Task HandleGetGameServerQuery_NotExistingIdQueryGiven_ShouldReturnNotFoundError(GetGameServerQuery query)
        {
            // Arrange
            var validator = new GetGameServerQueryValidator();

            // Act (& Assert validator)
            var validatorResult = await validator.ValidateAsync(query);
            validatorResult.IsValid.Should().BeTrue();

            var queryResult = await _handler.Handle(query, default);

            // Assert
            queryResult.IsError.Should().BeTrue();
            queryResult.FirstError.Type.Should().Be(ErrorOr.ErrorType.NotFound);
            _testEnvironment.MockGameServerRepository.Verify(x => x.GetGameServer(It.IsAny<GameServerId>()), Times.Once);
        }

        public static IEnumerable<object[]> ValidIdsGetGameServerQueries()
        {
            var testEnvironment = UnitTestEnvironments.GameServerTestEnvironment.Create();

            yield return new object[] { GetGameServerQueryUtils.Create()};
            yield return new object[] { GetGameServerQueryUtils.Create(testEnvironment.GameServers[1].Id.Value)};
        }
        public static IEnumerable<object[]> BadIdsGetGameServerQueries()
        {
            yield return new object[] { GetGameServerQueryUtils.Create(Guid.NewGuid())};
            yield return new object[] { GetGameServerQueryUtils.Create(Guid.NewGuid())};
        }
    }
}
