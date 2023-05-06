using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Shared.Contracts.Auth
{
    public sealed record RegisterRequest (
        string Email,
        string Password);
}
