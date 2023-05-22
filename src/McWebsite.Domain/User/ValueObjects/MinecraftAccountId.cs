using McWebsite.Domain.Common.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.User.ValueObjects
{
    public sealed class MinecraftAccountId : ValueObject
    {
        public int Value { get; }

        private MinecraftAccountId(int accountId)
        {
            Value = accountId;
        }

        public static MinecraftAccountId Create(int accountId)
        {
            return new MinecraftAccountId(accountId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
