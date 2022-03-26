using System;
using MediatR;
using UserManagement.Application.Responses;
using UserManagement.Core.Enums;

namespace UserManagement.Application.Commands
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
