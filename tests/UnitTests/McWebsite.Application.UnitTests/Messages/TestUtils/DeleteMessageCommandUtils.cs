using McWebsite.Application.Messages.Commands.DeleteMessageCommand;
using McWebsite.Application.UnitTests.TestUtils.Constants;
using McWebsite.Application.UnitTests.Messages.TestUtils;

namespace McWebsite.Application.UnitTests.Messages.TestUtils
{
    public static class DeleteMessageCommandUtils
    {
        public static DeleteMessageCommand Create(Guid? Id = null)
        {
            return new DeleteMessageCommand(Id ?? Constants.MessageQueriesAndCommands.Id);
        }
    }
}
