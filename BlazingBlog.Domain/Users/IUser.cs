using BlazingBlog.Domain.Articles;

namespace BlazingBlog.Domain.Users
{
    // Field names match Microsoft.Identity User
    public interface IUser
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<Article> Articles { get; set; }
    }
}
