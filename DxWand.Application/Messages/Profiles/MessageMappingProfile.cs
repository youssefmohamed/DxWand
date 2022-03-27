using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DxWand.Application.Messages.Commands;
using DxWand.Application.Messages.Responses;
using DxWand.Core.Entities;

namespace DxWand.Application.Messages.Profiles
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<CreateMessageResponse, CreateMessageCommand>().ReverseMap();
            CreateMap<Message, CreateMessageCommand>().ReverseMap();
            CreateMap<Message, CreateMessageResponse>().ReverseMap();

        }
    }
}
