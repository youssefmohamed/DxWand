using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands;

namespace UserManagement.API.Controllers
{
    [Route("api/user/access")]
    [ApiController]
    public class UserAccess : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserAccess(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand createUserCommand) 
        {
            var loginResult = await _mediator.Send(createUserCommand);
            return  new ObjectResult(loginResult) { StatusCode = loginResult.StatusCode };
        }

    }
}
