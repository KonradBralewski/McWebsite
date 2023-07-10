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
        public static class Persistence
        {
            public static Error UnitCreationError => Error.Unexpected(code: "SystemUnexpected.Persistence.UnitCreationError",
                                                                  description: "A database error occurred while creating unit.");

            public static Error UnitDeletionError => Error.Unexpected(code: "SystemUnexpected.Persistence.UnitDeletionError",
                                                                  description: "A database error occurred while deleting unit.");

            public static Error UnitUpdateError => Error.Unexpected(code: "SystemUnexpected.Persistence.UnitUpdateError",
                                                                    description: "A database error occured while updating unit.");
        }
    }
}
