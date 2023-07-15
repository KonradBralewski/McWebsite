using FluentValidation;

namespace McWebsite.Application.GameServerSubscriptions.Queries.GetGameServersSubscriptionsQuery
{
    public sealed class GetGameServersSubscriptionsQueryValidator : AbstractValidator<GetGameServersSubscriptionsQuery>
    {
        public GetGameServersSubscriptionsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(0).WithMessage("Page number has to be greater than or equal to 0.");
            RuleFor(x => x.EntriesPerPage).GreaterThanOrEqualTo(5).WithMessage("Entries per page has to be greater than or equal to 5.");
        }
    }
}
