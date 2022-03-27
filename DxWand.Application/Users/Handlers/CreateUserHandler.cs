using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DxWand.Application.Commands;
using DxWand.Application.Responses;
using DxWand.Application.Users.Responses;
using DxWand.Core.Entities;
using DxWand.Core.Enums;
using DxWand.Core.Repositories;
using FluentValidation;
using MediatR;

namespace DxWand.Application.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResponseMessage<CreateUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserCommand> _validator;
        public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IValidator<CreateUserCommand> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<ResponseMessage<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = await _validator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                return new ResponseMessage<CreateUserResponse>
                {
                    IsSuccess = false,
                    Message = validator.Errors.FirstOrDefault()?.ErrorMessage,
                    StatusCode = Convert.ToInt32(StatusCodeEnum.BadRequest)
                };
            }

            var applicationUser = _mapper.Map<ApplicationUser>(request);
            applicationUser.UserName = applicationUser.Email;
            applicationUser = await _userRepository.AddAsync(applicationUser);

            return new ResponseMessage<CreateUserResponse>
            {
                IsSuccess =  true,
                Data = _mapper.Map<CreateUserResponse>(applicationUser),
                StatusCode = Convert.ToInt32(StatusCodeEnum.Success),
                Message =  ""
            };


        }
    }
}
