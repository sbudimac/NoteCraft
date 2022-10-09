using AutoMapper;
using NoteCraftModels;

namespace NoteCraftAPI.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserCreateDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
