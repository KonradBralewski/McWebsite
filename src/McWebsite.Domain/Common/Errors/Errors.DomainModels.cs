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
        public static class DomainModels
        {
            public static Error ModelNotFound => Error.NotFound("DomainModels.NotFound", "The requested unit was not found.");
        }

    }
}
