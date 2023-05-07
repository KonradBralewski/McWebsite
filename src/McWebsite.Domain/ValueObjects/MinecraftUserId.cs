using McWebsite.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.ValueObjects
{
    public sealed record MinecraftUserId
    {
        public string Value { get; }

        public MinecraftUserId(string minecraftUserId)
        {
            if(string.IsNullOrWhiteSpace(minecraftUserId))
            {
                throw new MinecraftUserIdEmptyException();
            }

            Value = minecraftUserId;
        }

        public static implicit operator string(MinecraftUserId minecraftUserId) => minecraftUserId.Value;
        public static implicit operator MinecraftUserId(string minecraftUserId) => new MinecraftUserId(minecraftUserId);
    }
}
