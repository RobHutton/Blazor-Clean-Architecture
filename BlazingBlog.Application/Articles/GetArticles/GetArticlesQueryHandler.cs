namespace BlazingBlog.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IQueryHandler<GetArticlesQuery, List<ArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;
        public GetArticlesQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<Result<List<ArticleDto>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _articleRepository.GetAllArticlesAsync();
            return articles.Adapt<List<ArticleDto>>();
        }
    }
}
