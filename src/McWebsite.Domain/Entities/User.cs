using McWebsite.Domain.ValueObjects;
using McWebsite.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.Entities
{
    public sealed class User //: AggregateRoot<UserEmail>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public UserEmail Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string MinecraftUserId { get; set; } = null!;
    }
}
