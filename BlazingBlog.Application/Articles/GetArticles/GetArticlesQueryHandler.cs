using BlazingBlog.Domain.Users;

namespace BlazingBlog.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IQueryHandler<GetArticlesQuery, List<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        public GetArticlesQueryHandler(IArticleRepository articleRepository, IUserRepository userRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
        }
        public async Task<Result<List<ArticleDto>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            const string _default = "Unknown";
            var articles = await _articleRepository.GetAllArticlesAsync();
            List<string> userIds = articles.Where(a => a.UserId != null)
                .Select(a => a.UserId!)
                .Distinct()
                .ToList();
            var users = await _userRepository.GetUsersByIdsAsync(userIds);
            var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);
            var response = new List<ArticleDto>();
            foreach (var article in articles)
            {
                var articleDto = article.Adapt<ArticleDto>();
                if (article.UserId != null && userDictionary.TryGetValue(article.UserId, out var userName))
                {
                    articleDto.UserName = userName ?? _default;
                }
                else
                {
                    articleDto.UserName = _default;
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
