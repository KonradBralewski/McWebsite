using McWebsite.Domain.Common.Errors.SystemUnexpected;
using McWebsite.Infrastructure.Exceptions.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Exceptions
{
    public static partial class ExceptionsList
    {
        public static void ThrowIdenificationTryException()
        {
            var exception = McWebsiteInfrastructureException.Create(UnexpectedErrors.Identity.IdentifactionTryFailureError);
            throw exception;
        }

        /// <summary>
        /// Should never happen. Identity table & Table defining User domain bounded context are out of sync.
        /// </summary>
        public static void ThrowUserNotFoundButShouldBeException()
        {
            var exception = McWebsiteInfrastructureException.Create(UnexpectedErrors.Identity.UserNotFoundButShouldBeError);
            throw exception;
        }

    }
}
