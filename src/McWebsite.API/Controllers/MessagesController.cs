using MapsterMapper;
using McWebsite.API.Contracts.Message;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using McWebsite.Application.Messages.Commands.CreateMessageCommand;
using McWebsite.Application.Messages.Commands.DeleteMessageCommand;
using McWebsite.Application.Messages.Commands.UpdateMessageCommand;
using McWebsite.API.Controllers.Base;
using Mapster;
using McWebsite.Application.Messages.Queries.GetMessageQuery;
using McWebsite.Application.Messages.Queries.GetMessagesQuery;

namespace McWebsite.API.Controllers
{
    public class MessagesController : McWebsiteController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public MessagesController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetMessageById([FromRoute] Guid messageId)
        {
            var query = _mapper.Map<GetMessageQuery>(messageId);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                messageResult => Ok(messageResult.Adapt<GetMessageResponse>()),
                errors => Problem(errors));

        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesListAsync([FromQuery] int page, [FromQuery] int entriesPerPage)
        {
            var query = _mapper.Map<GetMessagesQuery>((page, entriesPerPage));

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                messagesResult => Ok(_mapper.Map<GetMessagesResponse>(messagesResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync([FromBody] CreateMessageRequest request)
        {
            var command = _mapper.Map<CreateMessageCommand>((this.RetrieveRequestSendingUserId(), request));

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                messageResult => Created($"api/Messages/{messageResult.Message.Id.Value}", _mapper.Map<CreateMessageResponse>(messageResult)),
                errors => Problem(errors));

        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessageAsync([FromRoute] Guid messageId)
        {
            var command = _mapper.Map<DeleteMessageCommand>(messageId);

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
                result => NoContent(),
                errors => Problem(errors));

        }

        [HttpPatch("{messageId}")]
        public async Task<IActionResult> UpdateMessageAsync([FromRoute] Guid messageId, [FromBody] UpdateMessageRequest request)
        {
            var command = _mapper.Map<UpdateMessageCommand>((messageId, request));

            var commandResult = await _mediator.Send(command);

            if (commandResult is null)
            {
                return NoContent();
            }

            return commandResult.Value.Match(
                result => Ok(_mapper.Map<UpdateMessageResponse>(result)),
                errors => Problem(errors));

        }

    }
}
