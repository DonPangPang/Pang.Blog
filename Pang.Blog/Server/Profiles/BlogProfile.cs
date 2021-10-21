using AutoMapper;
using Pang.Blog.Server.Entities;
using Pang.Blog.Shared;

namespace Pang.Blog.Server.Profiles
{
    public class BlogProfile: Profile
    {
        public BlogProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}