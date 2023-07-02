using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServers.Queries.GetGameServers
{
    public sealed class GetGameServersQueryValidator : AbstractValidator<GetGameServersQuery>
    {
        public GetGameServersQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(0);
            RuleFor(x => x.EntriesPerPage).GreaterThanOrEqualTo(5);
        }
    }
}
