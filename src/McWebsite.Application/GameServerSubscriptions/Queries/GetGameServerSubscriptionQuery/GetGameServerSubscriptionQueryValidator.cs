using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.GameServerSubscriptions.Queries.GetGameServerSubscriptionQuery
{
    public sealed class GetGameServerSubscriptionQueryValidator : AbstractValidator<GetGameServerSubscriptionQuery>
    {
        public GetGameServerSubscriptionQueryValidator()
        {
            RuleFor(x => x.GameServerSubscriptionId).NotEmpty().WithMessage("GameServerSubscriptionId cannot be empty.");
            RuleFor(x => x.GameServerSubscriptionId).Must(id => Guid.TryParse(id.ToString(), out _))
                                                    .WithMessage("GameServerSubscriptionId has to be a valid GUID value.");
        }
    }
}
