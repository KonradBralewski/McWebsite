using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.InGameEvent.Entities;
using McWebsite.Domain.InGameEvent.Enums;
using McWebsite.Domain.InGameEvent.ValueObjects;
using Moq;

namespace McWebsite.Application.UnitTests.TestEnvironments
{
    public static partial class UnitTestEnvironments
    {
        private static List<InGameEvent> InGameEvents = new List<InGameEvent>
            {
                InGameEvent.Recreate(Constants.InGameEventQueriesAndCommands.Id,
                                    Constants.GameServerQueriesAndCommands.Id,
                                    10009,
                                    EventType.PlayerEvent,
                                    "TestDescription NUMBER 1",
                                    2000,
                                    DateTime.UtcNow,
                                    DateTime.UtcNow),

                InGameEvent.Create(Constants.GameServerQueriesAndCommands.Id,
                                   10010,
                                   EventType.WeatherEvent,
                                   "TestDescription NUMBER 2",
                                   3000,
                                   DateTime.UtcNow,
                                   DateTime.UtcNow),
                InGameEvent.Create(Constants.GameServerQueriesAndCommands.Id,
                                   10011,
                                   EventType.TradeEvent,
                                   "TestDescription NUMBER 3",
                                   4000,
                                   DateTime.UtcNow,
                                   DateTime.UtcNow)
            };
        public class InGameEventTestEnvironment
        {
            public List<InGameEvent> InGameEvents = null!;
            public Mock<IInGameEventRepository> MockInGameEventRepository = null!;

            private InGameEventTestEnvironment()
            {
            }

            public static InGameEventTestEnvironment Create()
            {
                InGameEventTestEnvironment testEnvironment = new InGameEventTestEnvironment();

                testEnvironment.InGameEvents = new List<InGameEvent>(UnitTestEnvironments.InGameEvents);
                testEnvironment.MockInGameEventRepository = GetMock(testEnvironment.InGameEvents);

                return testEnvironment;
            }

            public static Mock<IInGameEventRepository> GetMock(List<InGameEvent> testCollection)
            {
                Mock<IInGameEventRepository> mock = new Mock<IInGameEventRepository>();

                mock.Setup(m => m.CreateInGameEvent(It.IsAny<InGameEvent>()))
                    .ReturnsAsync((InGameEvent inGameEvent) => {
                        testCollection.Add(inGameEvent);
                        return inGameEvent;
                    });

                mock.Setup(m => m.GetInGameEvent(It.IsAny<InGameEventId>())).ReturnsAsync((InGameEventId id)
                    =>
                {
                    if (testCollection.FirstOrDefault(gs => gs.Id.Value == id.Value) is not InGameEvent foundInGameEvent)
                    {
                        return Errors.DomainModels.ModelNotFound;
                    }

                    return foundInGameEvent;
                });

                mock.Setup(m => m.GetInGameEvents(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync((int page, int entriesPerPage) =>
                    testCollection.OrderByDescending(p => p.CreatedDateTime)
                        .Skip(page)
                        .Take(entriesPerPage)
                        .ToList());

                mock.Setup(m => m.DeleteInGameEvent(It.IsAny<InGameEvent>()))
                    .Returns((InGameEvent inGameEvent) =>
                    {
                        testCollection.RemoveAll(gsEntry => gsEntry.Id.Value == inGameEvent.Id.Value);
                        return Task.CompletedTask;
                    });

                mock.Setup(m => m.UpdateInGameEvent(It.IsAny<InGameEvent>()))
                    .ReturnsAsync((InGameEvent updatedInGameEvent) =>
                    {
                        int foundInGameEventIndex = testCollection.FindIndex(gs => gs.Id.Value == updatedInGameEvent.Id.Value);


                        testCollection[foundInGameEventIndex] = InGameEvent.Recreate(updatedInGameEvent.Id.Value,
                                                                         updatedInGameEvent.GameServerId.Value,
                                                                         updatedInGameEvent.InGameId,
                                                                         updatedInGameEvent.InGameEventType.Value,
                                                                         updatedInGameEvent.Description,
                                                                         updatedInGameEvent.Price,
                                                                         updatedInGameEvent.CreatedDateTime,
                                                                         updatedInGameEvent.UpdatedDateTime);
                        return updatedInGameEvent;
                    });

                return mock;
            }



        }
    }
}
