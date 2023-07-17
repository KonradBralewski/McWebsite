using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Conversations.Queries.GetConversationsQuery
{
    public sealed class GetConversationsQueryValidator : AbstractValidator<GetConversationsQuery>
    {
        public GetConversationsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(0).WithMessage("Page number has to be greater than or equal to 0.");
            RuleFor(x => x.EntriesPerPage).GreaterThanOrEqualTo(5).WithMessage("Entries per page has to be greater than or equal to 5.");
        }
    }
}
