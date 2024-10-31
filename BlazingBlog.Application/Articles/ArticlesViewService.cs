using BlazorCleanArchitecture.Application.Articles.GetArticles;
using BlazorCleanArchitecture.Application.Articles.TogglePublishArticle;
using MediatR;

namespace BlazorCleanArchitecture.Application.Articles
{
    public class ArticlesViewService : IArticlesViewService
    {
        private readonly ISender _sender;
        public ArticlesViewService(ISender sender)
        {
            _sender = sender;
        }
        public async Task<List<ArticleDto>?> GetArticlesByCurrentUserAsync()
        {
            var result = await _sender.Send(new GetArticlesByCurrentUserQuery());
            return result;
        }

        public async Task<ArticleDto?> TogglePublishArticleAsync(int articleId)
        {
            var result = await _sender.Send(new TogglePublishArticleCommand { ArticleId = articleId });
            return result;
        }
    }
}
