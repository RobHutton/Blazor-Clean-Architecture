using BlazingBlog.Application.Users;

namespace BlazingBlog.Application.Articles.GetArticleForEdit
{
    public class GetArticleForEditQueryHandler : IQueryHandler<GetArticleForEditQuery, ArticleDto?>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;
        public GetArticleForEditQueryHandler(IArticleRepository articleRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }
        public async Task<Result<ArticleDto?>> Handle(GetArticleForEditQuery request, CancellationToken cancellationToken)
        {
            var canEdit = await _userService.CurrentUserCanEditArticleAsync(request.Id);
            if (!canEdit)
            {
                return Result.ERR<ArticleDto?>("You are not authorized to edit this article.");
            }
            var article = await _articleRepository.GetArticleByIdAsync(request.Id);
            var articleDto = article.Adapt<ArticleDto>();
            return articleDto;
        }
    }
}
