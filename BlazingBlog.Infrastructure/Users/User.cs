using BlazingBlog.Domain.Articles;
using BlazingBlog.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace BlazingBlog.Infrastructure.Users
{
    public class User : IdentityUser, IUser
    {
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
