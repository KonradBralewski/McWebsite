using McWebsite.Domain.Common.DomainBase;
using McWebsite.Domain.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.User
{
    public sealed class User : AggregateRoot<UserId, Guid>
    {
        public MinecraftAccountId MinecraftAccountId { get; private set; }
        public UserEmail Email { get; private set; }
        public UserPassword Password { get; private set; }

        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }
        private User(UserId id,
                    MinecraftAccountId minecraftAccountId,
                    UserEmail email,
                    UserPassword password,
                    DateTime createdDateTime,
                    DateTime updatedDateTime) : base(id)
        {
            Id = id;
            MinecraftAccountId = minecraftAccountId;
            Password = password;
            Email = email;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }


        public static User Create(int? minecraftAccountId,
                                  string email,
                                  string password,
                                  DateTime createdDateTime,
                                  DateTime updatedDateTime)
        {
            return new User(UserId.CreateUnique(),
                            MinecraftAccountId.Create(minecraftAccountId),
                            UserEmail.Create(email),
                            UserPassword.Create(password),
                            createdDateTime,
                            updatedDateTime);
        }

        public static User Recreate(Guid id,
                                   int? minecraftAccountId,
                                   string email,
                                   string password,
                                   DateTime createdDateTime,
                                   DateTime updatedDateTime)
        {
            return new User(UserId.Create(id),
                            MinecraftAccountId.Create(minecraftAccountId),
                            UserEmail.Create(email),
                            UserPassword.Create(password),
                            createdDateTime,
                            updatedDateTime);
        }

        /// <summary>
        /// Constructor that will be used by EF Core, EF Core is not able to setup navigation property for Tuple<UserId, UserId>
        /// </summary>
#pragma warning disable CS8618
        private User()
        {

        }

#pragma warning restore CS8618

    }
}
