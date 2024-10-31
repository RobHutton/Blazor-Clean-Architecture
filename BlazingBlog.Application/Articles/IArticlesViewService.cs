namespace BlazorCleanArchitecture.Application.Articles
{
    public interface IArticlesViewService
    {
        Task<ArticleDto?> TogglePublishArticleAsync(int articleId);
        Task<List<ArticleDto>?> GetArticlesByCurrentUserAsync();
    }
}
