using AutoMapper;
using UserManagement.Application.Commands;
using UserManagement.Application.Responses;
using UserManagement.Core.Entities;

namespace UserManagement.Application.Profiles
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
        }
    }
}
