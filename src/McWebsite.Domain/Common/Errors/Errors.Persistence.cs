using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Persistence
        {
            public static Error UnitCreationError => Error.Unexpected(code: "Persistence.DatabaseError",
                                                                  description: "A database error occurred.");
        }
    }
}
