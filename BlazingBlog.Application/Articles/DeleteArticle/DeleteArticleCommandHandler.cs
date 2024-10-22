namespace BlazingBlog.Application.Articles.DeleteArticle
{
    public class DeleteArticleCommandHandler : ICommandHandler<DeleteArticleCommand>
    {
        private readonly IArticleRepository _articleRepository;
        public DeleteArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<Result> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var ok = await _articleRepository.DeleteArticleAsync(request.Id);
            if (ok)
            {
                return Result.OK();
            }
            return Result.ERR("Failed to delete article.");
        }
    }
}
