using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DxWand.Application.Responses;
using DxWand.Application.Users.Queries;
using DxWand.Application.Users.Responses;
using DxWand.Core.Enums;
using DxWand.Core.Repositories;
using MediatR;

namespace DxWand.Application.Users.Handlers
{
    public class GetUserInfoHandler : IRequestHandler<GetUserInfoQuery, ResponseMessage<GetUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserInfoHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResponseMessage<GetUserResponse>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userResponse = await _userRepository.GetByIdAsync(request.Id);
            return new ResponseMessage<GetUserResponse>
            {
                IsSuccess = true,
                Message = "",
                Data = _mapper.Map<GetUserResponse>(userResponse),
                StatusCode = Convert.ToInt32(StatusCodeEnum.Success),
            };
        }
    }
}
