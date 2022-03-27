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
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, ResponseMessage<List<GetUserResponse>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResponseMessage<List<GetUserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var usersResponse = await _userRepository.GetAllAsync();
            return new ResponseMessage<List<GetUserResponse>> {
                IsSuccess = true,
                Message = "",
                Data = usersResponse.Select(x => new GetUserResponse { Id = x.Id, Email = x.Email, BirthDate = x.BirthDate, Gender = x.Gender }).ToList(),
                StatusCode = Convert.ToInt32(StatusCodeEnum.Success),
            };
        }
    }
}
