using McWebsite.Application.Exceptions.Base;
using McWebsite.Domain.Common.Errors.SystemUnexpected;

namespace McWebsite.Application.Exceptions
{
    public static partial class ExceptionsList
    {
        public static McWebsiteApplicationException ThrowAlreadyExistingUnitCreationTryException()
        {
            var exception = McWebsiteApplicationException.Create(UnexpectedErrors.Domain.ExistingUnitCreationTry);
            throw exception;
        }

    }
}
