using FluentValidation;

namespace McWebsite.Application.InGameEventOrders.Queries.GetInGameEventOrdersQuery
{
    public sealed class GetInGameEventOrdersQueryValidator : AbstractValidator<GetInGameEventOrdersQuery>
    {
        public GetInGameEventOrdersQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(0).WithMessage("Page number has to be greater than or equal to 0.");
            RuleFor(x => x.EntriesPerPage).GreaterThanOrEqualTo(5).WithMessage("Entries per page has to be greater than or equal to 5.");
        }
    }
}
