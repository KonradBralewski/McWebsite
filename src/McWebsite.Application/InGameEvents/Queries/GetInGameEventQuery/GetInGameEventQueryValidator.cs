using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.InGameEvents.Queries.GetInGameEventQuery
{
    public sealed class GetInGameEventQueryValidator : AbstractValidator<GetInGameEventQuery>
    {

        public GetInGameEventQueryValidator()
        {
            RuleFor(x => x.InGameEventId).NotEmpty().WithMessage("InGameEventId cannot be empty.");
            RuleFor(x => x.InGameEventId).Must(id => Guid.TryParse(id.ToString(), out _))
                                                    .WithMessage("InGameEventId has to be a valid GUID value.");
        }
    }
}
