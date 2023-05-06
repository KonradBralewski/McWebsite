using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid id, string email, string password);
    }
}
