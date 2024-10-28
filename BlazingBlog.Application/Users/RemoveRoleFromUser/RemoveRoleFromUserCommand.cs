namespace BlazingBlog.Application.Users.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommand : ICommand
    {
        public required string UserId { get; set; }
        public required string RoleName { get; set; }
    }
}
