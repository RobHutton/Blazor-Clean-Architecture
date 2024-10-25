using BlazingBlog.Application.Exceptions;
using BlazingBlog.Application.Users;

namespace BlazingBlog.Application.Articles.DeleteArticle
{
    public class DeleteArticleCommandHandler : ICommandHandler<DeleteArticleCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;
        public DeleteArticleCommandHandler(IArticleRepository articleRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }
        public async Task<Result> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _userService.CurrentUserCanEditArticleAsync(request.Id))
                {
                    return FailingResult("You are not authorized to delete this article.");
                }
                var ok = await _articleRepository.DeleteArticleAsync(request.Id);
                if (ok)
                {
                    return Result.OK();
                }
                return FailingResult("Failed to delete article.");
            }
            catch (UserNotAuthorizedException)
            {
                return FailingResult("An error occurred deleting the article.");
            }
        }
        private Result<ArticleDto> FailingResult(string msg)
        {
            return Result.ERR<ArticleDto>(msg ?? "Failed to delete article.");
        }
    }
}
