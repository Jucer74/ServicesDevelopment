using AutoMapper;
using Users.Application.Dtos.Users;
using Users.Domain.Entities;

namespace Users.Application.Mapping;

public class MappingUserProfile : Profile
{
    public MappingUserProfile()
    {
        CreateMap<UserDtoInput, User>();
        CreateMap<User, UserDtoOutput>();
    }
}