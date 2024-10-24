namespace BlazingBlog.WebUI.Server.Features.Articles
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public DateTime DatePublished { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = false;
        public string? UserName { get; set; }
    }
}
