using Microsoft.AspNetCore.Http;
using Pang.Blog.Server.Entities;
using Pang.Blog.Server.Extensions;

namespace Pang.Blog.Server.Helpers
{
    public static class LoginUserInfo
    {
        public static User? Get()
        {
            var session = new HttpContextAccessor().HttpContext?.Session;

            return session.GetString("LoginUserInfo").ToObject<User>();
        }

        public static void Set(User user)
        {
            var session = new HttpContextAccessor().HttpContext?.Session;

            session.SetString("LoginUserInfo", user.ToJson());
        }
    }
}