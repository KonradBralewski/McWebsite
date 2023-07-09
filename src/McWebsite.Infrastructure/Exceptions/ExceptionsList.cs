using McWebsite.Domain.Common.Errors;
using Microsoft.EntityFrameworkCore;

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


    }
}
