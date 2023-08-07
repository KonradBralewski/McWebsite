using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Domain.GameServerSubscription.Enums;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using Moq;

namespace McWebsite.Application.UnitTests.TestEnvironments
{
    public static partial class UnitTestEnvironments
    {
        private static List<GameServerSubscription> GameServersSubscriptions = new List<GameServerSubscription>
            {
                GameServerSubscription.Recreate(Constants.GameServerSubscriptionQueriesAndCommands.Id,
                                                Constants.GameServerSubscriptionQueriesAndCommands.GameServerId,
                                                SubscriptionType.ItemEnhanced,
                                                128876,
                                                5000,
                                                "TestDescription NUMBER 1",
                                                TimeSpan.FromHours(12),
                                                DateTime.UtcNow,
                                                DateTime.UtcNow),
                GameServerSubscription.Create(Constants.GameServerSubscriptionQueriesAndCommands.GameServerId,
                                              SubscriptionType.ItemEnhanced,
                                              128876,
                                              5000,
                                              "TestDescription NUMBER 1",
                                              TimeSpan.FromHours(12),
                                              DateTime.UtcNow,
                                              DateTime.UtcNow)
            };
        public class GameServerSubscriptionTestEnvironment
        {
            public List<GameServerSubscription> GameServersSubscriptions = null!;
            public Mock<IGameServerSubscriptionRepository> MockGameServerSubscriptionRepository = null!;
            private GameServerSubscriptionTestEnvironment()
            {
            }

            public static GameServerSubscriptionTestEnvironment Create()
            {
                GameServerSubscriptionTestEnvironment testEnvironment = new GameServerSubscriptionTestEnvironment();

                testEnvironment.GameServersSubscriptions = new List<GameServerSubscription>(UnitTestEnvironments.GameServersSubscriptions);
                testEnvironment.MockGameServerSubscriptionRepository = GetMock(testEnvironment.GameServersSubscriptions);

                return testEnvironment;
            }

            public static Mock<IGameServerSubscriptionRepository> GetMock(List<GameServerSubscription> testCollection)
            {
                Mock<IGameServerSubscriptionRepository> mock = new Mock<IGameServerSubscriptionRepository>();

                mock.Setup(m => m.CreateGameServerSubscription(It.IsAny<GameServerSubscription>()))
                    .ReturnsAsync((GameServerSubscription gameServerSubscription) =>
                    {
                        testCollection.Add(gameServerSubscription);
                        return gameServerSubscription;
                    });

                mock.Setup(m => m.GetGameServerSubscription(It.IsAny<GameServerSubscriptionId>())).ReturnsAsync((GameServerSubscriptionId Id)
                    =>
                {
                    if (testCollection.FirstOrDefault(gss => gss.Id.Value == Id.Value) is not GameServerSubscription foundGameServerSubscription)
                    {
                        return Errors.DomainModels.ModelNotFound;
                    }

                    return foundGameServerSubscription;
                });

                mock.Setup(m => m.GetGameServersSubscriptions(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync((int page, int entriesPerPage) =>
                    testCollection.OrderBy(p => p.CreatedDateTime)
                        .Skip(page)
                        .Take(entriesPerPage)
                        .ToList());

                mock.Setup(m => m.DeleteGameServerSubscription(It.IsAny<GameServerSubscription>()))
                    .Returns((GameServerSubscription gameServerSubscription) =>
                    {
                        testCollection.RemoveAll(gssEntry => gssEntry.Id.Value == gameServerSubscription.Id.Value);
                        return Task.CompletedTask;
                    });

                mock.Setup(m => m.UpdateGameServerSubscription(It.IsAny<GameServerSubscription>()))
                    .ReturnsAsync((GameServerSubscription updatedGameServerSubscription) =>
                    {
                        int foundServerSubscriptionIndex = testCollection.FindIndex(gss => gss.Id.Value == updatedGameServerSubscription.Id.Value);

                        testCollection[foundServerSubscriptionIndex] = GameServerSubscription.Recreate(updatedGameServerSubscription.Id.Value,
                                                                         updatedGameServerSubscription.GameServerId.Value,
                                                                         updatedGameServerSubscription.SubscriptionType.Value,
                                                                         updatedGameServerSubscription.InGameSubscriptionId,
                                                                         updatedGameServerSubscription.Price,
                                                                         updatedGameServerSubscription.SubscriptionDescription,
                                                                         updatedGameServerSubscription.SubscriptionDuration,
                                                                         updatedGameServerSubscription.CreatedDateTime,
                                                                         updatedGameServerSubscription.UpdatedDateTime);
                        return updatedGameServerSubscription;
                    });

                return mock;
            }



        }
    }
}
