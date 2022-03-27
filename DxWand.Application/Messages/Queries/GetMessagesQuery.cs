using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxWand.Application.Messages.Responses;
using DxWand.Application.Responses;
using MediatR;

namespace DxWand.Application.Messages.Queries
{
    public class GetMessagesQuery : IRequest<ResponseMessage<List<GetMessageResponse>>>
    {
        public string Id 
        {
            get; 
            set;
        }
    }
}
