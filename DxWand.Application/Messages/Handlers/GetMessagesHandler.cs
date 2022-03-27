using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DxWand.Application.Messages.Queries;
using DxWand.Application.Messages.Responses;
using DxWand.Application.Responses;
using DxWand.Core.Enums;
using DxWand.Core.Repositories;
using MediatR;

namespace DxWand.Application.Messages.Handlers
{
    public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, ResponseMessage<List<GetMessageResponse>>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public GetMessagesHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
        public async Task<ResponseMessage<List<GetMessageResponse>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var getMessagesResponse = await _messageRepository.GetByUserIdAsync(request.Id);
            return new ResponseMessage<List<GetMessageResponse>> {
                IsSuccess = true,
                Data = getMessagesResponse.Select(x => new GetMessageResponse { Lang = x.Lang, Content = x.Content }).ToList(),
                StatusCode = Convert.ToInt32(StatusCodeEnum.Success),
                Message = ""
            };
        }
    }
}
