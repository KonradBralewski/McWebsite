using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Infrastructure.Persistence.Identity
{
    public sealed class McWebsiteIdentityUser : IdentityUser
    {
        public override string UserName
        {
            get { return base.Email!; }
        }
    }
}
