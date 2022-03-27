using System;
using System.Threading;
using System.Threading.Tasks;
using DxWand.Application.Dashboard.Queries;
using DxWand.Application.Dashboard.Responses;
using DxWand.Application.Responses;
using DxWand.Core.Enums;
using DxWand.Core.Interfaces;
using MediatR;

namespace DxWand.Application.Dashboard.Handlers
{
    public class DashboardHandler : IRequestHandler<DashboardQuery, ResponseMessage<DashboardResponse>>
    {
        private readonly IStatistics<string> _messageStatistics;
        private readonly IStatistics<GenderEnum> _genderStatistics;
        private readonly IStatistics<AgeStatisticsEnum> _ageStatistics;
        public DashboardHandler(IStatistics<string> messageStatistics, IStatistics<GenderEnum> genderStatistics, IStatistics<AgeStatisticsEnum> ageStatistics)
        {
            _messageStatistics = messageStatistics;
            _ageStatistics = ageStatistics;
            _genderStatistics = genderStatistics;
        }
        public async Task<ResponseMessage<DashboardResponse>> Handle(DashboardQuery request, CancellationToken cancellationToken)
        {
            var dashboardResponse = new DashboardResponse();
            dashboardResponse.MessageStatistics = await _messageStatistics.BuildStatisticsModel();
            dashboardResponse.AgeStatistics = await _ageStatistics.BuildStatisticsModel();
            dashboardResponse.GenderStatistics = await _genderStatistics.BuildStatisticsModel();

            return new ResponseMessage<DashboardResponse>
            {
                IsSuccess = true,
                Data = dashboardResponse,
                StatusCode = Convert.ToInt32(StatusCodeEnum.Success),
            };
        }
    }
}
