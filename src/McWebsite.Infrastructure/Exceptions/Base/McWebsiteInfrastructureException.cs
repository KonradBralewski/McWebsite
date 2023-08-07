using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace McWebsite.Infrastructure.Exceptions.Base
{
    public class McWebsiteInfrastructureException : Exception
    {
        private McWebsiteInfrastructureException() { }
        public static McWebsiteInfrastructureException Create(ErrorOr.Error errorToReturn)
        {
            McWebsiteInfrastructureException exception = new McWebsiteInfrastructureException();

            exception.Data.Add("error", errorToReturn);

            return exception;
        }
    }
}
