using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.ValueObjects
{
    public sealed record UserEmail
    {
        public string Value { get; }
        public UserEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
               //throw new EmailEmptyException();
            }

            Value = email;
        }

        public static implicit operator string (UserEmail userEmail) => userEmail.Value;
        public static implicit operator UserEmail(string email) => new UserEmail(email);
    }
}
