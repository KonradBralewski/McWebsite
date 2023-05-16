﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.API.Contracts.Auth
{
    public sealed record AuthenticationResponse(
        Guid UserId,
        string UserEmail,
        string Token);
}
