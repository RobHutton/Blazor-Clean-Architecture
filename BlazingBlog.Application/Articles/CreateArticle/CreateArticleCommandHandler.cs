using BlazorCleanArchitecture.Application.Exceptions;
using BlazorCleanArchitecture.Application.Users;

namespace BlazorCleanArchitecture.Application.Articles.CreateArticle
{
    public class CreateArticleCommandHandler : ICommandHandler<CreateArticleCommand, ArticleDto>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;
        public CreateArticleCommandHandler(IArticleRepository articleRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }
        public async Task<Result<ArticleDto>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newArticle = request.Adapt<Article>();
                newArticle.UserId = await _userService.GetCurrentUserIdAsync();
                if (!await _userService.CurrentUserCanCreateArticleAsync())
                {
                    return FailingResult("You are not authorized to create an article.");
                }
                var article = await _articleRepository.CreateArticleAsync(newArticle);
                return article.Adapt<ArticleDto>();
            }
            catch (UserNotAuthorizedException)
            {
                return FailingResult("An error occurred creating the article.");
            }

        }
        private Result<ArticleDto> FailingResult(string msg)
        {
            return Result.Fail<ArticleDto>(msg ?? "Failed to create article.");
        }
    }
}
