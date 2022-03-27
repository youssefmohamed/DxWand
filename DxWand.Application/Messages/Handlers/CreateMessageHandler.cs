using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DxWand.Application.Messages.Commands;
using DxWand.Application.Messages.Responses;
using DxWand.Application.Responses;
using DxWand.Core.Entities;
using DxWand.Core.Enums;
using DxWand.Core.Repositories;
using FluentValidation;
using MediatR;
using NTextCat;

namespace DxWand.Application.Messages.Handlers
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, ResponseMessage<CreateMessageResponse>>
    {
        private readonly IValidator<CreateMessageCommand> _validator;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public CreateMessageHandler(IMessageRepository messageRepository, IValidator<CreateMessageCommand> validator, IMapper mapper)
        {
            _validator = validator;
            _mapper = mapper;
            _messageRepository = messageRepository;
        }
        public async Task<ResponseMessage<CreateMessageResponse>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var validator = await _validator.ValidateAsync(request);
            if (!validator.IsValid) 
            {
                return new ResponseMessage<CreateMessageResponse>
                {
                    IsSuccess = false,
                    Message = validator.Errors.FirstOrDefault()?.ErrorMessage,
                    StatusCode = Convert.ToInt32(StatusCodeEnum.BadRequest),
                    Data = null
                };
            }

            var message = _mapper.Map<Message>(request);

            var factory = new RankedLanguageIdentifierFactory();
            var identifier = factory.Load("Core14.profile.xml"); 
            var lang = identifier.Identify(request.Content);
            message.Lang = lang.FirstOrDefault()?.Item1?.Iso639_3;

            var createMessageResponse = await _messageRepository.AddAsync(message);

            return new ResponseMessage<CreateMessageResponse>
            {
                IsSuccess = true,
                Message = "",
                Data = _mapper.Map<CreateMessageResponse>(createMessageResponse),
                StatusCode = Convert.ToInt32(StatusCodeEnum.Success), 
            };
        }
    }
}
