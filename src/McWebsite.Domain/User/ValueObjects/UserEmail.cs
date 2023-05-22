using McWebsite.Domain.Common.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.User.ValueObjects
{
    public sealed class UserEmail : ValueObject
    {
        public string Value { get; }

        private UserEmail(string email)
        {
            Value = email;
        }

        public static UserEmail Create(string email)
        {
            return new UserEmail(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(UserEmail userEmail) => userEmail.Value;
        public static implicit operator UserEmail(string email) => UserEmail.Create(email);
    }
}
