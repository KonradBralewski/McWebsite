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
        public static class Domain
        {
            public static Error ExistingUnitCreationTry => Error.Unexpected(code: "SystemUnexpected.Domain.ExistingUnitCreationTry",
                                                               description: "An unexpected request was registered to create already existing unit.");
        }
    }
}
