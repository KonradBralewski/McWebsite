using FluentValidation;
using McWebsite.Application.GameServers.Queries.GetGameServers;

namespace McWebsite.Application.GameServers.Queries.GetGameServer
{
    public sealed class GetGameServerQueryValidator : AbstractValidator<GetGameServerQuery>
    {
        public GetGameServerQueryValidator()
        {
            RuleFor(x => x.GameServerId).NotEmpty().WithMessage("GameServerId cannot be empty.");

            RuleFor(x => x.GameServerId).Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("GameServerId has to be a valid guid value.");
        }
    }
}
