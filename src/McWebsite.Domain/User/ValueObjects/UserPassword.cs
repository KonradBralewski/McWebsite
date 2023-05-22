using McWebsite.Domain.Common.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.User.ValueObjects
{
    public sealed class UserPassword : ValueObject
    {
        public string Value { get; }

        private UserPassword(string email)
        {
            Value = email;
        }

        public static UserPassword Create(string email)
        {
            return new UserPassword(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(UserPassword userPassword) => userPassword.Value;
        public static implicit operator UserPassword(string email) => UserPassword.Create(email);
    }
}
