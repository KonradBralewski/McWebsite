using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Shared.Contracts.Auth
{
    public sealed record LoginRequest (
        string Email,
        string Password);
}
