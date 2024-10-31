using BlazorCleanArchitecture.Domain.Articles;
using BlazorCleanArchitecture.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace BlazorCleanArchitecture.Infrastructure.Users
{
    public class User : IdentityUser, IUser
    {
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
