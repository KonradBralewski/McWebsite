using ErrorOr;
using McWebsite.Domain.GameServer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Messages.Queries.GetMessagesQuery
{
    public sealed record GetMessagesQuery(int Page, int EntriesPerPage) : IRequest<ErrorOr<GetMessagesResult>>;
}
