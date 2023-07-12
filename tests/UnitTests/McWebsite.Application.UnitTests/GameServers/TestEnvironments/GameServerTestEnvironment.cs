using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.GameServer;
using McWebsite.Domain.GameServer.Enums;
using McWebsite.Domain.GameServer.ValueObjects;
using Moq;

namespace McWebsite.Application.UnitTests.GameServers.TestEnvironments
{
    public static partial class UnitTestEnvironments
    {
        private static Mock<IGameServerRepository> GetMock(List<GameServer> testCollection)
        {
            Mock<IGameServerRepository> mock = new Mock<IGameServerRepository>();

            mock.Setup(m => m.CreateGameServer(It.IsAny<GameServer>()))
                .ReturnsAsync((GameServer gameServer) => {
                        testCollection.Add(gameServer);
                        return gameServer;
                    });

            mock.Setup(m => m.GetGameServer(It.IsAny<GameServerId>())).ReturnsAsync((GameServerId Id)
                =>
            {
                if (testCollection.FirstOrDefault(gs => gs.Id.Value == Id.Value) is not GameServer foundGameServer)
                {
                    return Errors.DomainModels.ModelNotFound;
                }

                return foundGameServer;
            });
            mock.Setup(m => m.GetGameServers(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int page, int entriesPerPage) =>
                testCollection.OrderByDescending(p => p.CreatedDateTime)
                    .Skip(page)
                    .Take(entriesPerPage)
                    .ToList());

            mock.Setup(m => m.DeleteGameServer(It.IsAny<GameServer>()))
                .Returns((GameServer gameserver) =>
                {
                    testCollection.RemoveAll(gsEntry => gsEntry.Id.Value == gameserver.Id.Value);
                    return Task.CompletedTask;
                });

            mock.Setup(m => m.UpdateGameServer(It.IsAny<GameServer>()))
                .ReturnsAsync((GameServer updatedGameServer) =>
                {
                    int foundServerIndex = testCollection.FindIndex(gs => gs.Id.Value == updatedGameServer.Id.Value);


                    testCollection[foundServerIndex] = GameServer.Recreate(updatedGameServer.Id.Value,
                                                                     updatedGameServer.MaximumPlayersNumber,
                                                                     updatedGameServer.CurrentPlayersNumber,
                                                                     updatedGameServer.ServerLocation.Value,
                                                                     updatedGameServer.ServerType.Value,
                                                                     updatedGameServer.Description,
                                                                     updatedGameServer.CreatedDateTime,
                                                                     updatedGameServer.UpdatedDateTime);
                    return updatedGameServer;
                });

            return mock;
        }

        private static List<GameServer> GameServers = new List<GameServer>
            {
                GameServer.Recreate(Constants.GameServerQueriesAndCommands.Id,
                                    1000,
                                    50,
                                    ServerLocation.Antarctica,
                                    ServerType.Vanilla,
                                    "TestDescription NUMBER 1",
                                    DateTime.UtcNow,
                                    DateTime.UtcNow),
                GameServer.Create(3000,
                                  500,
                                  ServerLocation.Europe,
                                  ServerType.Spigot,
                                  "TestDescription NUMBER 2",
                                  DateTime.UtcNow,
                                  DateTime.UtcNow)
            };
        public class GameServerTestEnvironment
        {
            public List<GameServer> GameServers = null!;
            public Mock<IGameServerRepository> MockGameServerRepository = null!;

            private GameServerTestEnvironment()
            {
            }

            public static GameServerTestEnvironment Create()
            {
                GameServerTestEnvironment testEnvironment = new GameServerTestEnvironment();

                testEnvironment.GameServers = new List<GameServer>(UnitTestEnvironments.GameServers);
                testEnvironment.MockGameServerRepository = GetMock(testEnvironment.GameServers);

                return testEnvironment;
            }

            
            
        }
    }
}
