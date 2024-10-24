namespace BlazingBlog.Application.Users.RegisterUser
{
    public class RegisterUserCommand : ICommand
    {
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        public required string Password { get; set; }
    }
}
