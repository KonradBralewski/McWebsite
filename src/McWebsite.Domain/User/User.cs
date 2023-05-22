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
        public MinecraftAccountId MinecraftAccountId { get; }
        public UserEmail Email { get; }
        public UserPassword Password { get; }
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

                    
        public static User Create(MinecraftAccountId minecraftAccountId,
                                  string email,
                                  string password)
        {
            return new User(UserId.CreateUnique(), minecraftAccountId, UserEmail.Create(email), UserPassword.Create(password));
        }

    }
}
