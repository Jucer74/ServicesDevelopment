using AutoMapper;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Mappers
{
    public class MapperUser : Profile
    {
        public MapperUser()
        {
            CreateMap<UserDTO, User>().ReverseMap();

            CreateMap<CreateUserDto, User>();
        }
    }
}
