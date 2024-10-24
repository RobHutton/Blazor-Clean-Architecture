using BlazingBlog.Domain.Abstractions;

namespace BlazingBlog.Domain.Articles
{
    public class Article : Entity
    {
        public required string Title { get; set; }
        public string? Content { get; set; }
        public DateTime DatePublished { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = false;
        public string? UserId { get; set; }
    }
}
