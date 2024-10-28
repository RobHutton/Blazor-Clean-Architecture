namespace BlazingBlog.Application.Users.AddRoleToUser
{
    public class AddRoleToUserCommand : ICommand
    {
        public required string UserId { get; set; }
        public required string RoleName { get; set; }
    }
}
