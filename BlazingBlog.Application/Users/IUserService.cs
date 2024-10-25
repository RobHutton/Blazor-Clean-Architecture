namespace BlazingBlog.Application.Users
{
    public interface IUserService
    {
        Task<string> GetCurrentUserIdAsync();
        Task<bool> IsCurrentUserInRoleAsync(string roleName);
        Task<bool> CurrentUserCanCreateArticleAsync();
        Task<bool> CurrentUserCanEditArticleAsync(int articleId);
    }
}
