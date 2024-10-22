namespace BlazingBlog.Application.Articles.UpdateArticle
{
    public class UpdateArticleCommandHandler : ICommandHandler<UpdateArticleCommand, ArticleDto?>
    {
        private readonly IArticleRepository _articleRepository;
        public UpdateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<Result<ArticleDto?>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var updateArticle = request.Adapt<Article>();
            var result = await _articleRepository.UpdateArticleAsync(updateArticle);
            if (result is null)
            {
                return Result.ERR<ArticleDto?>("Failed to get article.");
            }
            return result.Adapt<ArticleDto>();
        }
    }
}
