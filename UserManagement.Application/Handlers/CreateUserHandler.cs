using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Application.Responses;
using UserManagement.Core.Entities;
using UserManagement.Core.Enums;
using UserManagement.Core.Repositories;

namespace UserManagement.Application.Handlers
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
            var validator = _validator.ValidateAsync(request);
            if (!validator.Result.IsValid)
            {
                return new ResponseMessage<CreateUserResponse>
                {
                    IsSuccess = false,
                    Message = validator.Result.Errors.FirstOrDefault()?.ErrorMessage,
                    StatusCode = Convert.ToInt32(StatusCodeEnum.BadRequest)
                };
            }

            var applicationUser = _mapper.Map<ApplicationUser>(request);
            applicationUser.UserName = applicationUser.Email;
            applicationUser = await _userRepository.AddAsync(applicationUser);

            return new ResponseMessage<CreateUserResponse>
            {
                IsSuccess = applicationUser == null ? false : true,
                Data = _mapper.Map<CreateUserResponse>(applicationUser),
                StatusCode = Convert.ToInt32(StatusCodeEnum.Success) ,
                Message = applicationUser == null ? "User not created" : ""
            };


        }
    }
}
