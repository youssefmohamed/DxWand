using System.Collections.Generic;
using AutoMapper;
using DxWand.Application.Commands;
using DxWand.Application.Responses;
using DxWand.Application.Users.Responses;
using DxWand.Core.Entities;

namespace DxWand.Application.Users.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser,CreateUserCommand>()
                .ForMember(src => src.Password, dis => dis.MapFrom(x => x.PasswordHash));

            CreateMap<CreateUserCommand, ApplicationUser>()
                .ForMember(dis => dis.PasswordHash, src => src.MapFrom(x => x.Password));

            CreateMap<CreateUserResponse, ApplicationUser>().ReverseMap();

            CreateMap<GetUserResponse, ApplicationUser>().ReverseMap();

        }
    }
}
