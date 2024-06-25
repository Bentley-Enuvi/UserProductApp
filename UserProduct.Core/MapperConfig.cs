using AutoMapper;
using UserProduct.Core.DTOs;
using UserProduct.Domain.Entities;

namespace UserProduct.Core
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
        
    }
}
