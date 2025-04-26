using AutoMapper;
using Users.Application.Dtos;
using Users.Domain.Entities;

namespace Users.Application.Mapping;

public class MappingUserProfile : Profile
{
    public MappingUserProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
    }
}