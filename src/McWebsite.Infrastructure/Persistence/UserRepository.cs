using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Persistence
{
    internal sealed class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }
    }
}
