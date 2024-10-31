namespace BlazorCleanArchitecture.Application.Articles.CreateArticle
{
    public class CreateArticleCommand : ICommand<ArticleDto>
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime DatePublished { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = false;
    }
}
