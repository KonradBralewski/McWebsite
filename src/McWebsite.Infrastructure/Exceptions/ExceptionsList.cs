using McWebsite.Domain.Common.Errors.SystemUnexpected;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace McWebsite.Infrastructure.Exceptions
{
    public static class ExceptionsList
    {
        public static McWebsiteException ThrowCreationException()
        {
            var exception =  new McWebsiteException(new DbUpdateException(), UnexpectedErrors.Persistence.UnitCreationError).Exception;
            throw exception;
        }

        public static McWebsiteException ThrowDeletionException()
        {
            var exception = new McWebsiteException(new DbUpdateException(), UnexpectedErrors.Persistence.UnitDeletionError).Exception;
            throw exception;
        }

        public static McWebsiteException ThrowUpdateException()
        {
            var exception = new McWebsiteException(new DbUpdateException(), UnexpectedErrors.Persistence.UnitUpdateError).Exception;
            throw exception;
        }

        // TO:DO make this class partial and seperate concerns
        public static McWebsiteException ThrowIdenificationTryException()
        {
            var exception = new McWebsiteException(new Exception(), UnexpectedErrors.Identity.IdentifactionTryFailureError).Exception;
            throw exception;
        }

    }
}
