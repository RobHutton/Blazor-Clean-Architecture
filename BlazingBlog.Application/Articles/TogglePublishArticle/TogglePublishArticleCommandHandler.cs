
using BlazingBlog.Application.Users;

namespace BlazingBlog.Application.Articles.TogglePublishArticle
{
    public class TogglePublishArticleCommandHandler : ICommandHandler<TogglePublishArticleCommand, ArticleDto>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;

        public TogglePublishArticleCommandHandler(IArticleRepository articleRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }

        public async Task<Result<ArticleDto>> Handle(TogglePublishArticleCommand request, CancellationToken cancellationToken)
        {
            if (!await _userService.CurrentUserCanEditArticleAsync(request.ArticleId))
            {
                return Result.Fail<ArticleDto>("You are not authorized to edit this article.");
            }

            var article = await _articleRepository.GetArticleByIdAsync(request.ArticleId);
            if (article is null)
            {
                return Result.Fail<ArticleDto>("Article does not exist.");
            }

            article.IsPublished = !article.IsPublished;
            article.DateUpdated = DateTime.Now;

            if (article.IsPublished)
            {
                article.DatePublished = DateTime.Now;
            }

            var response = await _articleRepository.UpdateArticleAsync(article);
            if (response is null)
            {
                return Result.Fail<ArticleDto>("Failed to update article.");
            }

            return response.Adapt<ArticleDto>();
        }
    }
}
