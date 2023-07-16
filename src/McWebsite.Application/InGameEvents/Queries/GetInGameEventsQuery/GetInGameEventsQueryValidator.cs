using FluentValidation;

namespace McWebsite.Application.InGameEvents.Queries.GetInGameEventsQuery
{
    public sealed class GetInGameEventsQueryValidator : AbstractValidator<GetInGameEventsQuery>
    {
        public GetInGameEventsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(0).WithMessage("Page number has to be greater than or equal to 0.");
            RuleFor(x => x.EntriesPerPage).GreaterThanOrEqualTo(5).WithMessage("Entries per page has to be greater than or equal to 5.");
        }
    }
}
