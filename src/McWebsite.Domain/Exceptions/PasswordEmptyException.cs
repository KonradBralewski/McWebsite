using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Exceptions
{
    internal sealed class PasswordEmptyException : Exception
    {
        public PasswordEmptyException() : base("Password cannot be empty.")
        {
        }
    }
}
