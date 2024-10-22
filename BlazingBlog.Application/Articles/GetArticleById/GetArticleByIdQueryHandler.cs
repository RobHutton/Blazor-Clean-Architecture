using BlazingBlog.Application.Articles.GetArticleById;

namespace BlazingBlog.Application.Articles.GetArticlebyId
{
    public class GetArticleByIdQueryHandler : IQueryHandler<GetArticleByIdQuery, ArticleDto?>
    {
        private readonly IArticleRepository _articleRepository;
        public GetArticleByIdQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<Result<ArticleDto?>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _articleRepository.GetArticleByIdAsync(request.Id);
            if (result is null)
            {
                return Result.ERR<ArticleDto?>("Failed to get article.");
            }
            return result.Adapt<ArticleDto>();
        }
    }
}
