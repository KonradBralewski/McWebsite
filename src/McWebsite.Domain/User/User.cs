using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.User
{
    public sealed class User : AggregateRoot<UserId>
    {
        public MinecraftAccountId MinecraftAccountId { get; private set; }
        public UserEmail Email { get; private set; }
        public UserPassword Password { get; private set; }
        private User(UserId id,
                    MinecraftAccountId minecraftAccountId,
                    UserEmail email,
                    UserPassword password) : base(id)
        {
            Id = id;
            MinecraftAccountId = minecraftAccountId;
            Password = password;
            Email = email;
        }

                    
        public static User Create(int? minecraftAccountId,
                                  string email,
                                  string password)
        {
            return new User(UserId.CreateUnique(), MinecraftAccountId.Create(minecraftAccountId), UserEmail.Create(email), UserPassword.Create(password));
        }

    }
}
