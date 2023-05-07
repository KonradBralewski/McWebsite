using McWebsite.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Domain.ValueObjects
{
    public sealed record UserPassword
    {
        public string Value { get; }
        public UserPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                throw new PasswordEmptyException();
            }

            Value = password;

        }
            public static implicit operator string(UserPassword userPassword) => userPassword.Value;
            public static implicit operator UserPassword(string password) => new UserPassword(password);
    }
}
