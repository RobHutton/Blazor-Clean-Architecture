namespace BlazorCleanArchitecture.Application.Users
{
    public interface IUserService
    {
        Task<string> GetCurrentUserIdAsync();
        Task<bool> IsCurrentUserInRoleAsync(string roleName);
        Task<bool> CurrentUserCanCreateArticleAsync();
        Task<bool> CurrentUserCanEditArticleAsync(int articleId);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task AddRoleToUserAsync(string userId, string roleName);
        Task RemoveRoleFromUserAsync(string userId, string roleName);
    }
}
