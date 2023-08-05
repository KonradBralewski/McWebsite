using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Domain.Common.Errors;
using McWebsite.Domain.User;
using McWebsite.Domain.User.ValueObjects;
using Moq;

namespace McWebsite.Application.UnitTests.TestEnvironments
{
    public static partial class UnitTestEnvironments
    {
        private static List<User> Users = new List<User>
            {
                //User.Recreate(Constants.UserQueriesAndCommands.Id,
                //                    Constants.GameServerQueriesAndCommands.Id,
                //                    10009,
                //                    EventType.PlayerEvent,
                //                    "TestDescription NUMBER 1",
                //                    2000,
                //                    DateTime.UtcNow,
                //                    DateTime.UtcNow),

                //User.Create(Constants.GameServerQueriesAndCommands.Id,
                //                   10010,
                //                   EventType.WeatherEvent,
                //                   "TestDescription NUMBER 2",
                //                   3000,
                //                   DateTime.UtcNow,
                //                   DateTime.UtcNow)
            };
        public class UserTestEnvironment
        {
            public List<User> Users = null!;
            public Mock<IUserRepository> MockUserRepository = null!;

            private UserTestEnvironment()
            {
            }

            public static UserTestEnvironment Create()
            {
                UserTestEnvironment testEnvironment = new UserTestEnvironment();

                testEnvironment.Users = new List<User>(UnitTestEnvironments.Users);
                testEnvironment.MockUserRepository = GetMock(testEnvironment.Users);

                return testEnvironment;
            }

            public static Mock<IUserRepository> GetMock(List<User> testCollection)
            {
                Mock<IUserRepository> mock = new Mock<IUserRepository>();

                mock.Setup(m => m.GetUser(It.IsAny<UserId>())).ReturnsAsync((UserId id)
                    =>
                {
                    if (testCollection.FirstOrDefault(gs => gs.Id.Value == id.Value) is not User foundUser)
                    {
                        return Errors.DomainModels.ModelNotFound;
                    }

                    return foundUser;
                });

                mock.Setup(m => m.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync((int page, int entriesPerPage) =>
                    testCollection.OrderByDescending(p => p.CreatedDateTime)
                        .Skip(page)
                        .Take(entriesPerPage)
                        .ToList());

                return mock;
            }



        }
    }
}
