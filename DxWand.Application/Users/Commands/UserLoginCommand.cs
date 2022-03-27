using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using DxWand.Application.Responses;

namespace DxWand.Application.Commands
{
    public class UserLoginCommand : IRequest<ResponseMessage<string>>
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
    }
}
