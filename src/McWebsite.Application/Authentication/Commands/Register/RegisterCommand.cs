using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Authentication.Commands.Register
{
    public sealed record RegisterCommand(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
