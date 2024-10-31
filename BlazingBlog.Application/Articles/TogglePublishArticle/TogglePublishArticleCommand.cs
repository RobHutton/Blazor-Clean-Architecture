namespace BlazorCleanArchitecture.Application.Articles.TogglePublishArticle
{
    public class TogglePublishArticleCommand : ICommand<ArticleDto>
    {
        public int ArticleId { get; set; }
    }
}

