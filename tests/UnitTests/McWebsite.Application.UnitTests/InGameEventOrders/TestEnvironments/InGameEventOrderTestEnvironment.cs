using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.InGameEventOrder;
using McWebsite.Domain.InGameEventOrder.ValueObjects;
using Moq;
using McWebsite.Application.UnitTests.TestEnvironments;

namespace McWebsite.Application.UnitTests.TestEnvironments
{
    public static partial class UnitTestEnvironments
    {
        private static List<InGameEventOrder> InGameEventOrders = new List<InGameEventOrder>
            {
                InGameEventOrder.Recreate(Constants.InGameEventOrderQueriesAndCommands.Id,
                                    Constants.InGameEventOrderQueriesAndCommands.BuyingUserId,
                                    Constants.InGameEventOrderQueriesAndCommands.BoughtInGameEventId,
                                    DateTime.UtcNow,
                                    DateTime.UtcNow),
                InGameEventOrder.Create(Constants.InGameEventOrderQueriesAndCommands.BuyingUserId,
                                        Constants.InGameEventOrderQueriesAndCommands.BoughtInGameEventId,
                                        DateTime.UtcNow,
                                        DateTime.UtcNow)
            };
        public class InGameEventOrderTestEnvironment
        {
            public List<InGameEventOrder> InGameEventOrders = null!;
            public Mock<IInGameEventOrderRepository> MockInGameEventOrderRepository = null!;

            private InGameEventOrderTestEnvironment()
            {
            }

            public static InGameEventOrderTestEnvironment Create()
            {
                InGameEventOrderTestEnvironment testEnvironment = new InGameEventOrderTestEnvironment();

                testEnvironment.InGameEventOrders = new List<InGameEventOrder>(UnitTestEnvironments.InGameEventOrders);
                testEnvironment.MockInGameEventOrderRepository = GetMock(testEnvironment.InGameEventOrders);

                return testEnvironment;
            }

            public static Mock<IInGameEventOrderRepository> GetMock(List<InGameEventOrder> testCollection)
            {
                Mock<IInGameEventOrderRepository> mock = new Mock<IInGameEventOrderRepository>();

                mock.Setup(m => m.CreateInGameEventOrder(It.IsAny<InGameEventOrder>()))
                    .ReturnsAsync((InGameEventOrder inGameEventOrder) =>
                    {
                        testCollection.Add(inGameEventOrder);
                        return inGameEventOrder;
                    });

                mock.Setup(m => m.GetInGameEventOrder(It.IsAny<InGameEventOrderId>())).ReturnsAsync((InGameEventOrderId Id)
                    =>
                {
                    if (testCollection.FirstOrDefault(gs => gs.Id.Value == Id.Value) is not InGameEventOrder foundInGameEventOrder)
                    {
                        return Errors.DomainModels.ModelNotFound;
                    }

                    return foundInGameEventOrder;
                });

                mock.Setup(m => m.GetInGameEventOrders(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync((int page, int entriesPerPage) =>
                    testCollection.OrderByDescending(p => p.OrderDate)
                        .Skip(page)
                        .Take(entriesPerPage)
                        .ToList());

                mock.Setup(m => m.DeleteInGameEventOrder(It.IsAny<InGameEventOrder>()))
                    .Returns((InGameEventOrder gameserver) =>
                    {
                        testCollection.RemoveAll(gsEntry => gsEntry.Id.Value == gameserver.Id.Value);
                        return Task.CompletedTask;
                    });

                mock.Setup(m => m.UpdateInGameEventOrder(It.IsAny<InGameEventOrder>()))
                    .ReturnsAsync((InGameEventOrder updatedInGameEventOrder) =>
                    {
                        int inGameEventOrderIndex = testCollection.FindIndex(gs => gs.Id.Value == updatedInGameEventOrder.Id.Value);


                        testCollection[inGameEventOrderIndex] = InGameEventOrder.Recreate(updatedInGameEventOrder.Id.Value,
                                                                         updatedInGameEventOrder.BuyingUserId.Value,
                                                                         updatedInGameEventOrder.BoughtInGameEventId.Value,
                                                                         updatedInGameEventOrder.OrderDate,
                                                                         updatedInGameEventOrder.UpdatedDateTime);
                        return updatedInGameEventOrder;
                    });

                return mock;
            }



        }
    }
}
