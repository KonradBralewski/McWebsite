using Mapster;
using MapsterMapper;
using McWebsite.API.Contracts.Conversation;
using McWebsite.API.Controllers.Base;
using McWebsite.Application.Conversations.Commands.CreateConversationCommand;
using McWebsite.Application.Conversations.Commands.DeleteConversationCommand;
using McWebsite.Application.Conversations.Queries.GetConversationQuery;
using McWebsite.Application.Conversations.Queries.GetConversationsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace McWebsite.API.Controllers
{
    public class ConversationsController : McWebsiteController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public ConversationsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{conversationId}")]
        public async Task<IActionResult> GetConversationById([FromRoute] Guid conversationId)
        {
            var query = _mapper.Map<GetConversationQuery>(conversationId);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                conversationResult => Ok(conversationResult.Adapt<GetConversationResponse>()),
                errors => Problem(errors));

        }

        [HttpGet]
        public async Task<IActionResult> GetConversationsListAsync([FromQuery] int page, [FromQuery] int entriesPerPage)
        {
            var query = _mapper.Map<GetConversationsQuery>((page, entriesPerPage));

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                conversationsResult => Ok(_mapper.Map<GetConversationsResponse>(conversationsResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversationAsync([FromBody] CreateConversationRequest request)
        {
            var command = _mapper.Map<CreateConversationCommand>((this.RetrieveRequestSendingUserId(), request));

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                conversationResult => Created($"api/Conversations/{conversationResult.Conversation.Id.Value}",
                _mapper.Map<CreateConversationResponse>(conversationResult)),
                errors => Problem(errors));

        }

        [HttpDelete("{conversationId}")]
        public async Task<IActionResult> DeleteConversationAsync([FromRoute] Guid conversationId)
        {
            var command = _mapper.Map<DeleteConversationCommand>(conversationId);

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                result => NoContent(),
                errors => Problem(errors));

        }

    }
}
