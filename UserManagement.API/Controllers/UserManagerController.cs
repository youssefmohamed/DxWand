using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands;

namespace UserManagement.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/user/manage")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand)
        {
            var createUserResult = await _mediator.Send(createUserCommand);
            return new ObjectResult(createUserResult) { StatusCode = createUserResult.StatusCode };
        }
    }
}
