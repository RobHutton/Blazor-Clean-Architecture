namespace BlazingBlog.Application.Users
{
    public record struct UserDto
    (
        string Id,
        string UserName,
        string Email,
        string Roles
    )
    { }
}
