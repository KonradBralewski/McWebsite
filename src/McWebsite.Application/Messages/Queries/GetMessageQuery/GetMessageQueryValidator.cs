using FluentValidation;

namespace McWebsite.Application.Messages.Queries.GetMessageQuery
{
    public sealed class GetMessageQueryValidator : AbstractValidator<GetMessageQuery>
    {
        public GetMessageQueryValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty().WithMessage("MessageId cannot be empty.");

            RuleFor(x => x.MessageId).Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("MessageId has to be a valid guid value.");
        }
    }
}
