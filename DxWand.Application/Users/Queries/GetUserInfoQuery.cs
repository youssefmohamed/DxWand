using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxWand.Application.Responses;
using DxWand.Application.Users.Responses;
using MediatR;

namespace DxWand.Application.Users.Queries
{
    public class GetUserInfoQuery : IRequest<ResponseMessage<GetUserResponse>>
    {
        public string Id 
        {
            get; 
            set; 
        }
    }
}
