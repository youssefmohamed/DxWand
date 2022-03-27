using System.Threading.Tasks;
using DxWand.Application.Dashboard.Queries;
using DxWand.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DxWand.API.Controllers
{
    [Authorize(Roles = nameof(UserRolesEnum.Admin))]
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> Statistics()
        {
            var dashboardStatisticsResult = await _mediator.Send(new DashboardQuery());
            return new ObjectResult(dashboardStatisticsResult) { StatusCode = dashboardStatisticsResult.StatusCode };
        }
    }
}
