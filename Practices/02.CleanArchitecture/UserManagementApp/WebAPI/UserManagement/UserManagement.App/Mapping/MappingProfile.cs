using AutoMapper;
using UserManagement.App.Dtos;
using UserManagement.Dom.Entities;

namespace UserManagement.App.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
    }
}