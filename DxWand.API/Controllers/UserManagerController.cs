using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DxWand.Application.Commands;
using DxWand.Core.Enums;
using DxWand.Application.Users.Queries;
using System.Linq;

namespace DxWand.API.Controllers
{
    [Authorize(Roles = nameof(UserRolesEnum.Admin))]
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var getAllUsersResult = await _mediator.Send(new GetUsersQuery());
            return new ObjectResult(getAllUsersResult) { StatusCode = getAllUsersResult.StatusCode };
        }

        [HttpGet("getinfo")]
        public async Task<IActionResult> GetUserInfo(string id) 
        {
            var userInfoResponse = await _mediator.Send(new GetUserInfoQuery() { Id = id });
            return new ObjectResult(userInfoResponse) { StatusCode = userInfoResponse.StatusCode };
        }

    }
}
