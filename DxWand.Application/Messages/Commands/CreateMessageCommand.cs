using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxWand.Application.Messages.Responses;
using DxWand.Application.Responses;
using MediatR;

namespace DxWand.Application.Messages.Commands
{
    public class CreateMessageCommand : IRequest<ResponseMessage<CreateMessageResponse>>
    {
        public string Content
        {
            get;
            set;
        }
        public string UserId
        {
            get;
            set;
        }
    }
}
