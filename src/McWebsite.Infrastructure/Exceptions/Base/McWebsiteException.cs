using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Exceptions.Base
{
    public class McWebsiteException
    {
        public Exception Exception { get; private set; }

        public McWebsiteException(Exception exception, ErrorOr.Error errorToReturn)
        {
            exception.Data.Add("error", errorToReturn);
            Exception = exception;
        }
    }
}
