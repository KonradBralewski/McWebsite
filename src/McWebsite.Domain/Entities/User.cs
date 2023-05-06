using McWebsite.Domain.ValueObjects;
using McWebsite.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Entities
{
    internal sealed class User : AggregateRoot<UserEmail>
    {
        public UserEmail Email { get; private set; }
        private UserPassword _password;
        private MinecraftUserId _minecraftUserId;
    }
}
