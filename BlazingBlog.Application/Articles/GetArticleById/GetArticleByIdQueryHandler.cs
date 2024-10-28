using BlazingBlog.Application.Articles.GetArticleById;
using BlazingBlog.Application.Users;
using BlazingBlog.Domain.Users;

namespace BlazingBlog.Application.Articles.GetArticlebyId
{
    public class GetArticleByIdQueryHandler : IQueryHandler<GetArticleByIdQuery, ArticleDto?>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public GetArticleByIdQueryHandler(IArticleRepository articleRepository, IUserRepository userRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _userService = userService;
        }
        public async Task<Result<ArticleDto?>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            const string _default = "Unknown";
            var article = await _articleRepository.GetArticleByIdAsync(request.Id);
            if (article is null)
            {
                return Result.Fail<ArticleDto?>("Failed to get article.");
            }
            var articleDto = article.Adapt<ArticleDto>();
            if (article.UserId is not null)
            {
                var author = await _userRepository.GetUserByIdAsync(article.UserId);
                articleDto.UserName = author?.UserName ?? _default;
                articleDto.UserId = article.UserId;
                articleDto.CanEdit = await _userService.CurrentUserCanEditArticleAsync(article.Id);
            }
            return articleDto;
        }
    }
}
