using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Exceptions.Base
{
    public class McWebsiteApplicationException : Exception
    {
        private McWebsiteApplicationException() { }
        public static McWebsiteApplicationException Create(ErrorOr.Error errorToReturn)
        {
            McWebsiteApplicationException exception = new McWebsiteApplicationException();

            exception.Data.Add("error", errorToReturn);

            return exception;
        }
    }
}
