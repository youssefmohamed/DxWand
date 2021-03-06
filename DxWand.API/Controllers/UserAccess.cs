using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DxWand.Application.Commands;

namespace DxWand.API.Controllers
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
        public async Task<IActionResult> Login([FromBody] UserLoginCommand userLoginCommand) 
        {
            var loginResult = await _mediator.Send(userLoginCommand);
            return  new ObjectResult(loginResult) { StatusCode = loginResult.StatusCode };
        }

    }
}
