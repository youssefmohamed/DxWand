using DxWand.Application.Dashboard.Responses;
using DxWand.Application.Responses;
using MediatR;

namespace DxWand.Application.Dashboard.Queries
{
    public class DashboardQuery : IRequest<ResponseMessage<DashboardResponse>>
    {

    }
}
