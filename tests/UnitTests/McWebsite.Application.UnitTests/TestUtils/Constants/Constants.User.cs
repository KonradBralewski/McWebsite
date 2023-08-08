namespace McWebsite.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class UserQueriesAndCommands
        {
            public static Guid Id = Guid.NewGuid();
            public static Guid SecondUserId = Guid.NewGuid(); // Helpful with methods that require two users

            public static int MinecraftAccountId = 123456789;
            public static string Email = "Test_user1234@McWebsite.com";
            public static string Password = "Str0ngestP@ssw0rdOnE@rth!4768!;";
        }


    }
}
