using McWebsite.Domain.Common.Errors.SystemUnexpected;
using McWebsite.Infrastructure.Exceptions.Base;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace McWebsite.Infrastructure.Exceptions
{
    public static partial class ExceptionsList
    {
        public static void ThrowCreationException()
        {
            var exception = McWebsiteInfrastructureException.Create(UnexpectedErrors.Persistence.UnitCreationError);
            throw exception;
        }

        public static void ThrowDeletionException()
        {
            var exception =  McWebsiteInfrastructureException.Create(UnexpectedErrors.Persistence.UnitDeletionError);
            throw exception;
        }

        public static void ThrowUpdateException()
        {
            var exception = McWebsiteInfrastructureException.Create(UnexpectedErrors.Persistence.UnitUpdateError);
            throw exception;
        }

        public static void ThrowUnitBoundToEventNotFound()
        {
            var exception = McWebsiteInfrastructureException.Create(UnexpectedErrors.Persistence.UnitUpdateError);
            throw exception;
        }

    }
}
