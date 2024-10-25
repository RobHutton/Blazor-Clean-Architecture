using BlazingBlog.Application.Exceptions;
using BlazingBlog.Application.Users;
using BlazingBlog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlazingBlog.Infrastructure.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly ArticleRepository _articleRepository;

        public UserService(HttpContextAccessor httpContextAccessor, UserManager<User> userManager, ArticleRepository articleRepository)
        {
            _userManager = userManager;
            _articleRepository = articleRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CurrentUserCanCreateArticleAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user is null)
            {
                throw new UserNotAuthorizedException();
            }
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            var isWriter = await _userManager.IsInRoleAsync(user, "Writer");
            return isAdmin || isWriter;
        }

        public async Task<bool> CurrentUserCanEditArticleAsync(int articleId)
        {
            var user = await GetCurrentUserAsync();
            if (user is null)
            {
                return false;
            }
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            var isWriter = await _userManager.IsInRoleAsync(user, "Writer");
            var article = await _articleRepository.GetArticleByIdAsync(articleId);
            if (article is null)
            {
                return false;
            }
            return isAdmin || (isWriter && user.Id == article.UserId);
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user is null)
            {
                throw new UserNotAuthorizedException();
            }
            return user.Id;
        }

        public async Task<bool> IsCurrentUserInRoleAsync(string role)
        {
            var user = await GetCurrentUserAsync();
            var result = user is not null && await _userManager.IsInRoleAsync(user, role);
            return result;
        }
        private async Task<User?> GetCurrentUserAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null || httpContext.User is null)
            {
                return null;
            }
            return await _userManager.GetUserAsync(httpContext.User);
        }
    }
}
