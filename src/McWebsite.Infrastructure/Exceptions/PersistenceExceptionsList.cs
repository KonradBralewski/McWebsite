using McWebsite.Domain.Common.Errors.SystemUnexpected;
using McWebsite.Infrastructure.Exceptions.Base;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace McWebsite.Infrastructure.Exceptions
{
    public static partial class ExceptionsList
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

        public static McWebsiteException ThrowUnitBoundToEventNotFound()
        {
            var exception = new McWebsiteException(new DbUpdateException(), UnexpectedErrors.Persistence.UnitUpdateError).Exception;
            throw exception;
        }

    }
}
