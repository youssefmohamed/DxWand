using System;
using MediatR;
using DxWand.Application.Responses;
using DxWand.Core.Enums;
using DxWand.Application.Users.Responses;

namespace DxWand.Application.Commands
{
    public class CreateUserCommand : IRequest<ResponseMessage<CreateUserResponse>>
    {
        public string Email
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public GenderEnum Gender 
        {
            get; 
            set; 
        }
        public DateTime BirthDate 
        {
            get;
            set;
        }
    }
}
