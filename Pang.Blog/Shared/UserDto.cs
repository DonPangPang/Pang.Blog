namespace Pang.Blog.Shared
{
    public class UserDto
    {
        public string Name { get; set; } = null!;
        public string Account { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string OldPassword { get; set; }
    }
}