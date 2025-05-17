using AutoMapper;
using UserManagement.Domain.Entities;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}
