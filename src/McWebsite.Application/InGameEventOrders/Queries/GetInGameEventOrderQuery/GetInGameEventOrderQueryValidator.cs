using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrderQuery
{
    public sealed class GetInGameEventOrderQueryValidator : AbstractValidator<GetInGameEventOrderQuery>
    {

        public GetInGameEventOrderQueryValidator()
        {
            RuleFor(x => x.InGameEventOrderId).NotEmpty().WithMessage("InGameEventOrderId cannot be empty.");
            RuleFor(x => x.InGameEventOrderId).Must(id => Guid.TryParse(id.ToString(), out _))
                                                    .WithMessage("InGameEventOrderId has to be a valid GUID value.");
        }
    }
}
