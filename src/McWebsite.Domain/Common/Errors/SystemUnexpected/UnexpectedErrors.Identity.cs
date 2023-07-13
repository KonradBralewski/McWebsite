using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Common.Errors.SystemUnexpected
{
    public static partial class UnexpectedErrors
    {
        public static class Identity
        {
            public static Error IdentifactionTryFailureError => Error.Custom((int)CustomErrorsCodes.Codes.Identity,
                                                                             code: "SystemUnexpected.Identity.IdentifactionTryFailureError",
                                                                             description: "Unexpected error has occured while trying to retrieve user identification.");
            public static Error UserNotFoundButShouldBe => Error.Custom((int)CustomErrorsCodes.Codes.Identity,
                                                                             code: "SystemUnexpected.Identity.UserNotFoundButShouldBe",
                                                                             description: "Unexpected error has occured while trying to retrieve information about existing user.");
        }
    }
}
