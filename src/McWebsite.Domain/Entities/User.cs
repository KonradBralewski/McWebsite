using McWebsite.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Entities
{
    internal sealed class User
    {
        public UserEmail Email { get; private set; }
        private UserPassword _password;
        private MinecraftUserId _minecraftUserId;
    }
}
