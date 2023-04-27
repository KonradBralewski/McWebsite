using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Exceptions
{
    internal sealed class EmailEmptyException : Exception
    {
        public EmailEmptyException() : base("Email cannot be empty.")
        {
        }
    }
}
