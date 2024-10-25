using BlazingBlog.Application.Users;
using BlazingBlog.Domain.Users;

namespace BlazingBlog.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IQueryHandler<GetArticlesQuery, List<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public GetArticlesQueryHandler(IArticleRepository articleRepository, IUserRepository userRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _userService = userService;
        }
        public async Task<Result<List<ArticleDto>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            const string _default = "Unknown";
            var articles = await _articleRepository.GetAllArticlesAsync();
            // This makes more sense, but it breaks the clean architecture
            //List<string> userIds = articles.Where(a => a.UserId != null)
            //    .Select(a => a.UserId!)
            //    .Distinct()
            //    .ToList();
            //var users = await _userRepository.GetUsersByIdsAsync(userIds);
            //var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);
            var response = new List<ArticleDto>();
            foreach (var article in articles)
            {
                var articleDto = article.Adapt<ArticleDto>();
                if (article.UserId != null)
                {
                    var author = await _userRepository.GetUserByIdAsync(article.UserId);
                    articleDto.UserName = author?.UserName ?? _default;
                    articleDto.UserId = article.UserId;
                    articleDto.CanEdit = await _userService.CurrentUserCanEditArticleAsync(article.Id);
                }
                else
                {
                    articleDto.UserName = _default;
                    articleDto.CanEdit = false;
                }
                response.Add(articleDto);
            }
            // alternate only using IdentityManager
            //foreach (var article in articles)
            //{
            //    var articleDto = article.Adapt<ArticleDto>();
            //    if (article.UserId is not null)
            //    {
            //        var author = await _userRepository.GetUserByIdAsync(article.UserId);
            //        articleDto.UserName = author?.UserName ?? _default;
            //    }
            //    else
            //    {
            //        articleDto.UserName = _default;
            //    }
            //    response.Add(articleDto);
            //}
            return response.OrderByDescending(e => e.DatePublished).ToList();
        }
    }
}
