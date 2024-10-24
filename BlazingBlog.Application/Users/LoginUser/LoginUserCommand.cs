namespace BlazingBlog.Application.Users.LoginUser
{
    public class LoginUserCommand : ICommand
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
