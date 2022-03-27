using System.Linq;
using System.Threading.Tasks;
using DxWand.Application.Messages.Commands;
using DxWand.Application.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DxWand.API.Controllers
{
    [Authorize]
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateMessageCommand createMessageCommand) 
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value;
            createMessageCommand.UserId = userId;
            var message = await _mediator.Send(createMessageCommand);
            return new ObjectResult(message) { StatusCode = message.StatusCode };
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetUserMessages(string id) 
        {
            var userid = string.IsNullOrWhiteSpace(id) ?
                HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value 
                : id;
            var getMessagesResult = await _mediator.Send(new GetMessagesQuery() { Id = userid });
            return new ObjectResult(getMessagesResult) { StatusCode = getMessagesResult.StatusCode };
        }

    }
}
