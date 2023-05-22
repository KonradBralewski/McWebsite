using McWebsite.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Authentication.Commands
{
    public sealed record AuthenticationResult(User User, string Token);
}
