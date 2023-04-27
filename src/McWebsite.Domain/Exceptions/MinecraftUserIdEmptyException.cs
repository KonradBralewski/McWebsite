using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Exceptions
{
    internal sealed class MinecraftUserIdEmptyException : Exception
    {
        public MinecraftUserIdEmptyException() : base("Minecraft user id cannot be empty.")
        {
        }
    }
}
