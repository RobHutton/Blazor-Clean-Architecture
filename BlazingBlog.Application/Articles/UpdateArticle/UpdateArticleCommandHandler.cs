using BlazingBlog.Application.Exceptions;
using BlazingBlog.Application.Users;

namespace BlazingBlog.Application.Articles.UpdateArticle
{
    public class UpdateArticleCommandHandler : ICommandHandler<UpdateArticleCommand, ArticleDto?>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;
        public UpdateArticleCommandHandler(IArticleRepository articleRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }
        public async Task<Result<ArticleDto?>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateArticle = request.Adapt<Article>();
                if (!await _userService.CurrentUserCanEditArticleAsync(request.Id))
                {
                    return Result.ERR<ArticleDto?>("You are not authorized to edit this article.");
                }
                var result = await _articleRepository.UpdateArticleAsync(updateArticle);
                if (result is null)
                {
                    return Result.ERR<ArticleDto?>("Failed to get article.");
                }
                return result.Adapt<ArticleDto>();
            }
            catch (UserNotAuthorizedException)
            {
                return Result.ERR<ArticleDto?>("An error occurred updating the article.");
            }
        }
    }
}
